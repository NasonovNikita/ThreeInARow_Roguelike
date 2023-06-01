using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool randomSeed;
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

        Player.data = ScriptableObject.CreateInstance<PlayerData>();
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
            LoadSave();
            switch (PlayerPrefs.GetString("scene"))
            {
                case "Map":
                    LoadMap();
                    break;
                case "Battle":
                    StartCoroutine(LoadBattle());
                    break;
                case "Shop":
                    LoadShop();
                    break;
            }
        }
        else
        {
            ResetAll();
            SceneManager.LoadScene("Map");
        }
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void EnterMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void SaveData()
    {
        string playerData = JsonUtility.ToJson(Player.data);
        PlayerPrefs.SetInt("vertex", Map.currentVertex);
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("PlayerData", playerData);
    }

    private void ResetAll()
    {
        Map.currentVertex = -1;
        Player.data = Instantiate(Resources.Load<PlayerData>("Presets/NewGamePreset"));
        
        PlayerPrefs.DeleteAll();
    }

    private void LoadSave()
    {
        Map.currentVertex = PlayerPrefs.GetInt("vertex");
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerData"), Player.data);
    }

    private void LoadMap()
    {
        SceneManager.LoadScene("Map");
    }

    private IEnumerator<WaitUntil> LoadBattle()
    {
        SceneManager.LoadScene("Map");
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        Map map = FindFirstObjectByType<Map>();
        map.CurrentVertex_().OnArrive();
    }

    private void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }
}