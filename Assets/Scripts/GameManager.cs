using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class GameManager
{
    private static Stats _playerStats;

    private static bool _start;

    [RuntimeInitializeOnLoadMethod]
    public static void Any()
    {
        _playerStats = Resources.Load<Stats>("RuntimeData/PlayerStats");
        if (!_start)
        {
            ResetAllScriptableObjects();
        }
        _start = true;
    }

    public static void Restart()
    {
        Map.CurrentVertex = -1;
        ResetAllScriptableObjects();
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

    private static void ResetAllScriptableObjects()
    {
        _playerStats.Reset();
    }
}