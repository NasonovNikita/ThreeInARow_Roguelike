using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class GameManager
{
    private static Stats _playerStats;

    [RuntimeInitializeOnLoadMethod]
    private static void MainMenu()
    {
        _playerStats = Resources.Load<Stats>("RuntimeData/PlayerStats");
        GameObject.Find("New Game").GetComponent<Button>().onClick.AddListener(NewGame);
        GameObject.Find("Continue").GetComponent<Button>().onClick.AddListener(Continue);
    }

    private static void NewGame()
    {
        Debug.unityLogger.Log("NewGame");
        _playerStats.Reset();
        SceneManager.LoadScene("Map");
        PlayerPrefs.DeleteAll();
    }

    private static void Continue()
    {
        Debug.unityLogger.Log("Continue");
        if (PlayerPrefs.HasKey("vertex"))
        {
            Map.currentVertex = PlayerPrefs.GetInt("vertex");
        }

        SceneManager.LoadScene("Map");
    }

    public static void Restart()
    {
        Map.currentVertex = -1;
        SceneManager.LoadScene("Map");
    }

    public static void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt("vertex", Map.currentVertex);
    }
}