using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ToggleAltBattleUI : MonoBehaviour
    {
        private Toggle toggle;
        public void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.isOn = Globals.Instance.altBattleUI;
        }

        public void SetUI(bool val)
        {
            Globals.Instance.altBattleUI = val;
        }
    }
}