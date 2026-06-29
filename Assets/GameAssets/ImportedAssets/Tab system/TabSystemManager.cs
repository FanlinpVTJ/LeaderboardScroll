using System.Collections.Generic;
using UnityEngine;

namespace TabSystem
{
    public class TabSystemManager : MonoBehaviour
    {
        [SerializeField] private TabButton _defaultTab;
        [SerializeField] private List<TabButton> _tabs;

        private void Start()
        {
            if (_defaultTab != null)
            {
                SetTab(_defaultTab);
            }
        }

        public void SetTab(TabButton tab)
        {
            foreach (var t in _tabs)
            {
                if (!t) continue;

                t.SetActive(t == tab && t.IsActiveTab);
            }
        }
    }
}