using UnityEngine;
using Zenject;

public class LeaderboardIconProvidersInstaller : MonoInstaller
{
    [SerializeField] private TierIconProvider _tierIconProvider;
    [SerializeField] private PositionIconProvider _positionIconProvider;
    [SerializeField] private GuildIconProvider _guildIconProvider;

    public override void InstallBindings()
    {
        Container.Bind<ITierIconProvider>().FromInstance(_tierIconProvider).AsSingle();
        Container.Bind<IPositionIconProvider>().FromInstance(_positionIconProvider).AsSingle();
        Container.Bind<IGuildIconProvider>().FromInstance(_guildIconProvider).AsSingle();
    }
}
