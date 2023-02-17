using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Stats playerStats;

    public void Restart()
    {
        SceneManager.LoadScene("Match3");
        playerStats.Reset();
    }

    public void Exit()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
                playerStats.Reset();
            #else
                Application.Quit();
                playerStats.Reset();
            #endif
        }
}