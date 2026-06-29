using UnityEngine;
using Zenject;

namespace TabSystem
{
    [RequireComponent(typeof(TabSystemManager))]
    public class TabSystemManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TabSystemManager>().FromComponentSibling().AsSingle();
        }
    }
}
