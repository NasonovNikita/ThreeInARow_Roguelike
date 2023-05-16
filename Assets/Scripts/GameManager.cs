using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);

        Player.data = new PlayerData();
    }

    public void MainMenu()
    {
        AudioManager.instance.StopAll();
        
        SceneManager.LoadScene("MainMenu");
        
        AudioManager.instance.Play(AudioEnum.MainMenu);
    }
    
    public void NewGame()
    {
        ResetAll();
        SceneManager.LoadScene("Map");
    }

    public void Continue()
    {
        if (PlayerPrefs.HasKey("vertex"))
        {
            Map.currentVertex = PlayerPrefs.GetInt("vertex");
            Player.data = JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString("PlayerData"));
            
            if (PlayerPrefs.GetString("scene") == "Map")
            {
                LoadMap();
            }
            else if (PlayerPrefs.GetString("scene") == "Battle")
            {
                StartCoroutine(LoadBattle());
            }
        }
        else
        {
            ResetAll();
            SceneManager.LoadScene("Map");
        }
    }

    public void Restart()
    {
        Map.currentVertex = -1;
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

    public void SaveData()
    {
        string playerData = JsonConvert.SerializeObject(Player.data);
        PlayerPrefs.SetInt("vertex", Map.currentVertex);
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("PlayerData", playerData);
    }

    public void ResetAll()
    {
        Map.currentVertex = -1;
        Player.data = new PlayerData();
        
        PlayerPrefs.DeleteAll();
    }

    public void LoadMap()
    {
        SceneManager.LoadScene("Map");
    }

    public IEnumerator<WaitUntil> LoadBattle()
    {
        SceneManager.LoadScene("Map");
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        Map map = FindObjectOfType<Map>();
        map.CurrentVertex_().OnArrive();
    }
}