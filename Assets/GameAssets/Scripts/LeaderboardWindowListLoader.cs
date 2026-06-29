using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class LeaderboardWindowListLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loader;
    [SerializeField] private Transform _content;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

    private int _loadVersion;

    [Inject] private DiContainer _container;
    [Inject] private ILeaderboardPlayerListProvider _leaderboardPlayerListProvider;

    private void OnEnable()
    {
        _loadVersion++;
        LoadLeaderboardAsync(_loadVersion).Forget();
    }

    public void Reload()
    {
        _loadVersion++;
        LoadLeaderboardAsync(_loadVersion).Forget();
    }

    private async UniTaskVoid LoadLeaderboardAsync(int loadVersion)
    {
        SetLoadingState(true);
        ClearLeaderboard();

        List<PlayerInfoPayload> playerInfoList = await _leaderboardPlayerListProvider.LoadPlayerInfoListAsync();

        if (loadVersion != _loadVersion)
        {
            return;
        }

        SpawnLeaderboard(playerInfoList);
        SetLoadingState(false);
    }

    private void SetLoadingState(bool isLoading)
    {
        _loader.SetActive(isLoading);
        _content.gameObject.SetActive(!isLoading);
    }

    private void ClearLeaderboard()
    {
        for (int i = _content.childCount - 1; i >= 0; i--)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }

    private void SpawnLeaderboard(List<PlayerInfoPayload> playerInfoList)
    {
        for (int i = 0; i < playerInfoList.Count; i++)
        {
            LeaderboardElement leaderboardElement = _container.InstantiatePrefabForComponent<LeaderboardElement>(_leaderboardElementPrefab, _content);
            leaderboardElement.SetupElement(playerInfoList[i]);
        }
    }
}
