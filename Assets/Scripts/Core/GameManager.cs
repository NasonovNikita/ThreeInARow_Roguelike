using Audio;
using Battle.Units;
using Core.Saves;
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
                instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            Player.Data = ScriptableObject.CreateInstance<PlayerData>();
        }

        public void Start()
        {
            SettingsSave.Load();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");

            AudioManager.Instance.StopAll();

            AudioManager.Instance.Play(AudioEnum.MainMenu);

            SettingsSave.Save();
        }

        public void NewGame()
        {
            GameSave.CreateEmptySave().Apply();
        }

        public void Continue()
        {
            GameSave.Load();
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