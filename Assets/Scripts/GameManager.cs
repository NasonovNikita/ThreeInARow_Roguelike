using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Stats playerStats;

    public void Restart()
    {
        Map.CurrentVertex = -1;
        playerStats.Reset();
        SceneManager.LoadScene("Map");
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