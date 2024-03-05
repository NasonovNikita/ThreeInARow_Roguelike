using System.Collections.Generic;
using Audio;
using Battle;
using Battle.Units;
using Core.Saves;
using Other;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        
            DontDestroyOnLoad(gameObject);

            Player.data = ScriptableObject.CreateInstance<PlayerData>();
        }

        public void Start()
        {
            SavesManager.LoadSettings();
        }

        public void MainMenu()
        {
            if (SceneManager.GetActiveScene().name == "Battle")
            {
                Log.UnAttach();
            }
        
            SceneManager.LoadScene("MainMenu");
        
            AudioManager.instance.StopAll();
        
            AudioManager.instance.Play(AudioEnum.MainMenu);
        
            SavesManager.SaveSettings();
        }
    
        public void NewGame()
        {
            GameSave newSave = new GameSave().CreateEmptySave();
            newSave.Load();
        }

        public void Continue()
        {
            if (!SavesManager.TryLoadGameOrFail()) NewGame();
        }

        public void Settings()
        {
            SceneManager.LoadScene("Settings");
        }

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            PlayerPrefs.Save();
            Application.Quit();
        }

        public void EnterMap()
        {
            SceneManager.LoadScene("Map");
        }
    }
}