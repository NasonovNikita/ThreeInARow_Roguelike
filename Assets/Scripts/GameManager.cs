using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MapGenerator generator;

    public bool randomSeed;

    public int seed;

    private bool generated;
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
            if(!generated) GenerateMap();
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
        PlayerPrefs.SetInt("vertex", Map.currentVertex);
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(Player.data));
        PlayerPrefs.SetInt("seed", seed);
        PlayerPrefs.SetString("group", JsonUtility.ToJson(BattleManager.group));
    }

    private void ResetAll()
    {
        Map.currentVertex = -1;
        Player.data = Instantiate(Resources.Load<PlayerData>("Presets/NewGamePreset"));
        
        if (randomSeed) seed = Random.Range(0, 10000000);
        GenerateMap();
        
        PlayerPrefs.DeleteAll();
    }

    private void LoadSave()
    {
        Map.currentVertex = PlayerPrefs.GetInt("vertex");
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerData"), Player.data);
        seed = PlayerPrefs.GetInt("seed");
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("group"), BattleManager.group);
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

    private void GenerateMap()
    {
        generator = FindFirstObjectByType<MapGenerator>();
        Map.map = generator.GetMap(seed);
        generated = true;
    }
}