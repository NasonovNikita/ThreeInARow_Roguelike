using System;
using UnityEngine;

namespace Core.Saves
{
    [Serializable]
    public class SettingsSave : Save
    {
        [SerializeField] private float volume;
        [SerializeField] private float difficulty;
        [SerializeField] private bool altBattleUI;

        public SettingsSave()
        {
            volume = Globals.instance.volume;
            difficulty = Globals.instance.difficulty;
            altBattleUI = Globals.instance.altBattleUI;
        }

        public override void Load()
        {
            Globals.instance.volume = volume;
            Globals.instance.difficulty = difficulty;
            Globals.instance.altBattleUI = altBattleUI;
        }
    }
}