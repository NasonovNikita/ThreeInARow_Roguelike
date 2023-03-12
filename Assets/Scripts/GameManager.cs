using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Stats playerStats;

    private static bool _start;

    public void Awake()
    {
        if (!_start)
        {
            ResetAllScriptableObjects();
        }
        _start = true;
    }

    public void Restart()
    {
        Map.CurrentVertex = -1;
        ResetAllScriptableObjects();
        SceneManager.LoadScene("Map");
    }

    public void Exit()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        }

    private void ResetAllScriptableObjects()
    {
        playerStats.Reset();
    }
}