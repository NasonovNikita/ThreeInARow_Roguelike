using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Stats playerStats;

    [SerializeField]
    private BattleData battle;

    public void Restart()
    {
        Map.CurrentVertex = -1;
        ResetAllScriptableObjects();
        SceneManager.LoadScene("Map");
    }

    public void Exit()
        {
            #if UNITY_EDITOR
                ResetAllScriptableObjects();
                EditorApplication.ExitPlaymode();
            #else
                ResetAllSCriptableObjects();
                Application.Quit();
            #endif
        }

    private void ResetAllScriptableObjects()
    {
        playerStats.Reset();
        battle.Reset();
    }
}