using UnityEngine;
using Zenject;

public class LeaderboardPlayerListProviderInstaller : MonoInstaller
{
    [SerializeField] private TextAsset _playerInfoJsonFile;

    public override void InstallBindings()
    {
        Container.Bind<ILeaderboardPlayerListProvider>().To<LeaderboardPlayerListProvider>().AsSingle().WithArguments(_playerInfoJsonFile);
    }
}
