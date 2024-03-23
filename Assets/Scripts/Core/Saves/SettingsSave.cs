using System;
using UnityEngine;

namespace Core.Saves
{
    [Serializable]
    public class SettingsSave : SaveObject
    {
        [SerializeField] private float volume;
        [SerializeField] private float difficulty;
        [SerializeField] private bool altBattleUI;

        private const string Path = "/Settings.dat";

        public SettingsSave()
        {
            volume = Globals.instance.volume;
            difficulty = Globals.instance.difficulty;
            altBattleUI = Globals.instance.altBattleUI;
        }

        public static void Save()
        {
            SavesManager.Save(new SettingsSave(), Path);
        }

        public static void Load()
        {
            SavesManager.Load<SettingsSave>(Path).Apply();
        }
        public override void Apply()
        {
            Globals.instance.volume = volume;
            Globals.instance.difficulty = difficulty;
            Globals.instance.altBattleUI = altBattleUI;
        }
    }
}