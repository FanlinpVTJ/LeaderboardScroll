using UnityEngine;
using Zenject;

public class CurrentPlayerProfileProviderInstaller : MonoInstaller
{
    [SerializeField] private string _currentPlayerId = "player_001";

    public override void InstallBindings()
    {
        Container.Bind<ICurrentPlayerProfileProvider>()
            .To<CurrentPlayerProfileProvider>()
            .AsSingle()
            .WithArguments(_currentPlayerId);
    }
}
