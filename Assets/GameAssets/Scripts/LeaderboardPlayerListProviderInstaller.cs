using UnityEngine;
using Zenject;

public class LeaderboardPlayerListProviderInstaller : MonoInstaller
{
    [SerializeField] private TextAsset _playerInfoJsonFile;
    [SerializeField] [Min(0)] private int _loadingDelayMilliseconds = 1500;

    public override void InstallBindings()
    {
        Container.Bind<ILeaderboardPlayerListProvider>()
            .To<LeaderboardPlayerListProvider>()
            .AsSingle()
            .WithArguments(_playerInfoJsonFile, _loadingDelayMilliseconds);
    }
}
