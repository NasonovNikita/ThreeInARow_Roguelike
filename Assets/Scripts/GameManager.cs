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
            SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
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
        PlayerPrefs.SetString("goods", JsonUtility.ToJson(ShopManager.goods));
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
        BattleManager.group = ScriptableObject.CreateInstance<EnemyGroup>();
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("group"), BattleManager.group);
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("goods"), ShopManager.goods);
    }

    private void LoadMap()
    {
        SceneManager.LoadScene("Map");
    }

    private void LoadBattle()
    {
        SceneManager.LoadScene("Battle");
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