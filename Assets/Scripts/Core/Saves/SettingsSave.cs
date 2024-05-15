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
            volume = Globals.Instance.volume;
            difficulty = Globals.Instance.difficulty;
            altBattleUI = Globals.Instance.altBattleUI;
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
            Globals.Instance.volume = volume;
            Globals.Instance.difficulty = difficulty;
            Globals.Instance.altBattleUI = altBattleUI;
        }
    }
}