using Audio;
using UnityEngine;

namespace Core
{
    public class SettingsManager : MonoBehaviour
    {
        public void Awake()
        {
            AudioManager.instance.StopAll();
        
            AudioManager.instance.Play(AudioEnum.MainMenu);
        }

        public static void LoadSettings()
        {
            if (!PlayerPrefs.HasKey("volume")) return;
        
            Globals.instance.volume = PlayerPrefs.GetFloat("volume");
            Globals.instance.difficulty = PlayerPrefs.GetFloat("difficulty");
            Globals.instance.altBattleUI = PlayerPrefs.GetInt("AltBattleUI") == 1;
        }

        public static void SaveSettings()
        {
            PlayerPrefs.SetFloat("volume", Globals.instance.volume);
            PlayerPrefs.SetFloat("difficulty", Globals.instance.difficulty);
            PlayerPrefs.SetInt("AltBattleUI", Globals.instance.altBattleUI ? 1 : 0);
        }
    }
}