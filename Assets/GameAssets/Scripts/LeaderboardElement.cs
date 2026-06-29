using System.Collections.Generic;
using TMPro;
using SmartScroll;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _positionText;
    [SerializeField] private Image _positionImage;
    [SerializeField] private TextMeshProUGUI _playerLevelText;
    [SerializeField] private Image _playerLevelImage;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _playerScore;
    [SerializeField] private Image _guildTierIcon;
    [SerializeField] private TextMeshProUGUI _guildTierText;

    private SmartScrollElement _smartScrollElement;

    [Inject]
    private ITierIconProvider _tierIconProvider;
    [Inject]
    private IPositionIconProvider _positionIconProvider;
    [Inject]
    private IGuildIconProvider _guildTierIconProvider;
    [Inject]
    private ILeaderboardPlayerListProvider _leaderboardPlayerListProvider;

    private void Awake()
    {
        _smartScrollElement = GetComponent<SmartScrollElement>();
        _smartScrollElement.OnDataUpdated += UpdateElement;
    }

    private void OnDestroy()
    {
        _smartScrollElement.OnDataUpdated -= UpdateElement;
    }

    public void SetupElement(PlayerInfoPayload payload)
    {
        _positionText.text = payload.Position.ToString();
        _positionImage.sprite = _positionIconProvider.GetPositionIcon(payload.Position);

        _playerLevelText.text = payload.Level.ToString();
        _playerLevelImage.sprite = _tierIconProvider.GetTierIcon(payload.Level);

        _playerName.text = payload.Name;

        _playerScore.text = payload.Score.ToString();

        _guildTierIcon.sprite = _guildTierIconProvider.GetGuildTierIcon(payload.GuildTier);
        _guildTierText.text = payload.GuildTier.ToString();
    }

    private void UpdateElement()
    {
        PlayerInfoPayload payload = _leaderboardPlayerListProvider.GetPlayerInfoPayload(_smartScrollElement.Data.Index);

        SetupElement(payload);
    }
}
