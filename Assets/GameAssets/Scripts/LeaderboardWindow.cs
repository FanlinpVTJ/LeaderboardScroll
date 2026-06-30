using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SmartScroll;
using WindowsManager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LeaderboardWindow : Window
{
    [SerializeField] private SmartScrollViewDirectional _smartScrollView;
    [SerializeField] private RectTransform _viewport;
    [SerializeField] private LeaderboardElement _currentPlayerLeaderboardElementPrefab;
    [SerializeField] private RectTransform _currentPlayerLeaderboardElementRectSource;

    private int _loadVersion;
    private int _currentPlayerIndex = -1;
    private ScrollRect _scrollRect;
    private RectTransform _contentRectTransform;
    private RectTransform _viewportRectTransform;
    private LeaderboardElement _currentPlayerLeaderboardElement;
    private RectTransform _currentPlayerLeaderboardElementRectTransform;

    [Inject] private DiContainer _container;
    [Inject] private ILeaderboardPlayerListProvider _leaderboardPlayerListProvider;
    [Inject] private ICurrentPlayerProfileProvider _currentPlayerProfileProvider;

    protected override void Awake()
    {
        base.Awake();

        _scrollRect = _smartScrollView.GetComponent<ScrollRect>();
        _contentRectTransform = _scrollRect.content;
        _viewportRectTransform = _viewport;
        _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    private void OnDestroy()
    {
        _scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
    }

    protected override void OpenInner()
    {
        _loadVersion++;
        HideCurrentPlayerLeaderboardElement();
        LoadLeaderboardAsync(_loadVersion).Forget();
    }

    protected override void CloseInner()
    {
        _loadVersion++;
        _smartScrollView.Clear();
        HideCurrentPlayerLeaderboardElement();

        base.CloseInner();
    }

    private async UniTaskVoid LoadLeaderboardAsync(int loadVersion)
    {
        _smartScrollView.Clear();
        HideCurrentPlayerLeaderboardElement();

        List<PlayerInfoPayload> playerInfoList = await _leaderboardPlayerListProvider.LoadPlayerInfoListAsync();

        if (loadVersion != _loadVersion)
        {
            return;
        }

        _currentPlayerIndex = _leaderboardPlayerListProvider.GetPlayerIndex(_currentPlayerProfileProvider.GetCurrentPlayerId());
        _smartScrollView.CreateElements(playerInfoList.Count);
        await UniTask.DelayFrame(2);
        ScrollToCurrentPlayer();
        SetupCurrentPlayerLeaderboardElement();
        UpdateCurrentPlayerLeaderboardElementPosition();
    }

    private void SetupCurrentPlayerLeaderboardElement()
    {
        EnsureCurrentPlayerLeaderboardElement();

        PlayerInfoPayload currentPlayerPayload = _leaderboardPlayerListProvider.GetPlayerInfoPayload(_currentPlayerIndex);

        _currentPlayerLeaderboardElement.SetupElement(currentPlayerPayload);
        _currentPlayerLeaderboardElement.gameObject.SetActive(true);
        _currentPlayerLeaderboardElement.transform.SetAsLastSibling();
    }

    private void EnsureCurrentPlayerLeaderboardElement()
    {
        if (_currentPlayerLeaderboardElement != null)
        {
            return;
        }

        _currentPlayerLeaderboardElement = _container.InstantiatePrefabForComponent<LeaderboardElement>(_currentPlayerLeaderboardElementPrefab, _viewportRectTransform);
        _currentPlayerLeaderboardElementRectTransform = (RectTransform)_currentPlayerLeaderboardElement.transform;
        _currentPlayerLeaderboardElementRectTransform.anchorMin = new Vector2(0f, 1f);
        _currentPlayerLeaderboardElementRectTransform.anchorMax = new Vector2(0f, 1f);
        _currentPlayerLeaderboardElementRectTransform.pivot = new Vector2(0.5f, 0.5f);
        _currentPlayerLeaderboardElement.gameObject.SetActive(false);
    }

    private void HideCurrentPlayerLeaderboardElement()
    {
        _currentPlayerIndex = -1;

        if (_currentPlayerLeaderboardElement == null)
        {
            return;
        }

        _currentPlayerLeaderboardElement.gameObject.SetActive(false);
    }

    private void OnScrollValueChanged(Vector2 scrollPosition)
    {
        if (!IsOpen)
        {
            return;
        }

        if (_currentPlayerIndex < 0)
        {
            return;
        }

        if (_currentPlayerLeaderboardElement == null)
        {
            return;
        }

        UpdateCurrentPlayerLeaderboardElementPosition();
    }

    private void UpdateCurrentPlayerLeaderboardElementPosition()
    {
        float viewportWidth = _viewportRectTransform.rect.width;
        float viewportHeight = _viewportRectTransform.rect.height;
        float currentPlayerSourceHeight = _smartScrollView.ElementDatas[_currentPlayerIndex].GetSize();
        float currentPlayerCenterPositionY = -_smartScrollView.GetElementPosition(_currentPlayerIndex, false) - currentPlayerSourceHeight * 0.5f + _contentRectTransform.anchoredPosition.y;
        float currentPlayerHalfHeight = _currentPlayerLeaderboardElementRectTransform.rect.height * 0.5f;
        float minCurrentPlayerPositionY = currentPlayerHalfHeight - viewportHeight;
        float maxCurrentPlayerPositionY = -currentPlayerHalfHeight;
        float currentPlayerPositionY = Mathf.Clamp(currentPlayerCenterPositionY, minCurrentPlayerPositionY, maxCurrentPlayerPositionY);

        _currentPlayerLeaderboardElementRectTransform.anchoredPosition = new Vector2(viewportWidth * 0.5f, currentPlayerPositionY);
    }

    private void ScrollToCurrentPlayer()
    {
        float viewportHeight = _viewportRectTransform.rect.height;
        float currentPlayerSourceHeight = _currentPlayerLeaderboardElementRectSource.rect.height;
        float currentPlayerTopPosition = _smartScrollView.GetElementPosition(_currentPlayerIndex, false);
        float maxContentPositionY = Mathf.Max(0f, _contentRectTransform.rect.height - viewportHeight);
        float centeredContentPositionY = Mathf.Clamp(currentPlayerTopPosition - (viewportHeight - currentPlayerSourceHeight) * 0.5f, 0f, maxContentPositionY);
        float defaultContentPositionY = Mathf.Clamp(currentPlayerTopPosition, 0f, maxContentPositionY);
        float offset = centeredContentPositionY - defaultContentPositionY;

        _smartScrollView.ScrollToElement(_currentPlayerIndex, offset);
    }
}
