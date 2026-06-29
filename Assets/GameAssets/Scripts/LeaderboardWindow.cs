using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SmartScroll;
using WindowsManager;
using UnityEngine;
using Zenject;

public class LeaderboardWindow : Window
{
    [SerializeField] private SmartScrollViewDirectional _smartScrollView;

    private int _loadVersion;

    [Inject] private ILeaderboardPlayerListProvider _leaderboardPlayerListProvider;

    protected override void OpenInner()
    {
        _loadVersion++;
        LoadLeaderboardAsync(_loadVersion).Forget();
    }

    protected override void CloseInner()
    {
        _loadVersion++;
        _smartScrollView.Clear();

        base.CloseInner();
    }

    private async UniTaskVoid LoadLeaderboardAsync(int loadVersion)
    {
        _smartScrollView.Clear();

        List<PlayerInfoPayload> playerInfoList = await _leaderboardPlayerListProvider.LoadPlayerInfoListAsync();

        if (loadVersion != _loadVersion)
        {
            return;
        }

        _smartScrollView.CreateElements(playerInfoList.Count);
        _smartScrollView.ScrollToElement(0);
    }
}
