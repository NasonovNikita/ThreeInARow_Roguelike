using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class GameManager
{
    public static void NewGame()
    {
        Debug.unityLogger.Log("NewGame");
        
        Resources.Load<Stats>("RuntimeData/PlayerStats").Reset();
        SceneManager.LoadScene("Map");
        ResetAll();
    }

    public static void Continue()
    {
        Debug.unityLogger.Log("Continue");
        
        if (PlayerPrefs.HasKey("vertex"))
        {
            Map.currentVertex = PlayerPrefs.GetInt("vertex");
        }
        else
        {
            ResetAll();
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

    public static void ResetAll()
    {
        Map.currentVertex = -1;
        
        PlayerPrefs.DeleteAll();
    }
}