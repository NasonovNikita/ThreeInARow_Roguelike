using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Stats playerStats;
    [SerializeField]
    private MapData mapData;

    public void Restart()
    {
        SceneManager.LoadScene("Match3");
        playerStats.Reset();
        mapData.Reset();
    }

    public void Exit()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
                playerStats.Reset();
                mapData.Reset();
            #else
                Application.Quit();
                playerStats.Reset();
                mapData.Reset();
            #endif
        }
}