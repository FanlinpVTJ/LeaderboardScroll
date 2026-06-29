using UnityEngine;
using WindowsManager.UI;
using Zenject;

namespace TabSystem
{
    public class TabActivateButton : AbstractButton
    {
        [SerializeField]
        private TabButton _targetTab;

        [Inject]
        private TabSystemManager _tabSystem;

        public override void OnButtonClick()
        {
            _tabSystem.SetTab(_targetTab);
        }
    }
}
