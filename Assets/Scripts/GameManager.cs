using System.Collections.Generic;
using Newtonsoft.Json;
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
        Button[] buttons = Object.FindObjectsOfType<Button>();
        buttons[1].onClick.AddListener(NewGame);
        buttons[0].onClick.AddListener(Continue);
    }

    private static void NewGame()
    {
        _playerStats.Reset();
        SceneManager.LoadScene("Map");
    }

    private static void Continue()
    {
        if (PlayerPrefs.HasKey("vertex"))
        {
            Map.currentVertex = PlayerPrefs.GetInt("vertex");
            BattleManager.enemiesNames = JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString("enemies"));
            SceneManager.LoadScene(PlayerPrefs.GetString("scene"));   //Doesn't work for Battle, because there are no enemies (they are destroyed on quit)
        }
    }

    public static void Restart()
    {
        Map.currentVertex = -1;
        SceneManager.LoadScene("Map");
    }

    public static void Exit()
    {
        #if UNITY_EDITOR
            SaveData();
            EditorApplication.ExitPlaymode();
        #else
            SaveData();
            Application.Quit();
        #endif
    }

    private static void SaveData()
    {
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("vertex", Map.currentVertex);
        PlayerPrefs.SetString("enemies", JsonConvert.SerializeObject(BattleManager.enemiesNames));
    }
}