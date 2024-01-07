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
            toggle.isOn = Globals.instance.altBattleUI;
        }

        public void SetUI(bool val)
        {
            Globals.instance.altBattleUI = val;
        }
    }
}