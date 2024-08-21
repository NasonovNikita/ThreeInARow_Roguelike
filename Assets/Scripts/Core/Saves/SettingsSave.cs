using System;
using UnityEngine;

namespace Core.Saves
{
    /// <summary>
    ///     Contains data about settings.
    /// </summary>
    [Serializable]
    public class SettingsSave : SaveObject
    {
        private const string Path = "/Settings.dat";
        [SerializeField] private float volume = Globals.Instance.volume;
        [SerializeField] private float difficulty = Globals.Instance.difficulty;
        [SerializeField] private bool altBattleUI = Globals.Instance.altBattleUI;

        public static void Save()
        {
            SavesManager.Save(new SettingsSave(), Path);
        }

        /// <summary>
        ///     Loads data from memory.
        /// </summary>
        public static SettingsSave Load() =>
            SavesManager.Load<SettingsSave>(Path) ?? new SettingsSave();

        public override void Apply()
        {
            Globals.Instance.volume = volume;
            Globals.Instance.difficulty = difficulty;
            Globals.Instance.altBattleUI = altBattleUI;
        }
    }
}