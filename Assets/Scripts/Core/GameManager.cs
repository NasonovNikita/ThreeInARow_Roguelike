using Audio;
using Battle.Units;
using Core.Saves;
using UnityEditor;
using UnityEngine;

namespace Core
{
    /// <summary>
    ///     Is used for common actions.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            Player.Data = ScriptableObject.CreateInstance<PlayerData>();
        }

        public void Start()
        {
            SettingsSave.Load().Apply();
        }

        public void MainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

            AudioManager.Instance.StopAll();

            AudioManager.Instance.Play(AudioEnum.MainMenu);

            SettingsSave.Save();
        }

        public void NewGame()
        {
            GameSave.CreateEmptySave().Apply();
        }

        public void ContinueGameRun()
        {
            GameSave.Load().Apply();
        }

        public void GoToSettings()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
        }

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            PlayerPrefs.Save();
            Application.Quit();
        }

        public void GoToMap()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
        }
    }
}