using System;
using Audio;
using Battle;
using Battle.Units;
using Map;
using Shop;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public void Start()
    {
        SettingsManager.LoadSettings();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
        AudioManager.instance.StopAll();
        
        AudioManager.instance.Play(AudioEnum.MainMenu);
        
        SettingsManager.SaveSettings();
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

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
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
        PlayerPrefs.SetInt("vertex", Map.Map.currentVertex);
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(Player.data));
        PlayerPrefs.SetInt("seed", Globals.instance.seed);
        PlayerPrefs.SetString("group", JsonUtility.ToJson(BattleManager.group));
        PlayerPrefs.SetString("goods", JsonUtility.ToJson(ShopManager.goods));
    }

    private void ResetAll()
    {
        Map.Map.currentVertex = -1;
        Player.data = Instantiate(Resources.Load<PlayerData>("Presets/NewGamePreset"));
        
        if (Globals.instance.randomSeed) Globals.instance.seed = Random.Range(0, 10000000);
        GenerateMap();
        
        PlayerPrefs.DeleteAll();
    }

    private void LoadSave()
    {
        Map.Map.currentVertex = PlayerPrefs.GetInt("vertex");
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerData"), Player.data);
        Globals.instance.seed = PlayerPrefs.GetInt("seed");
        BattleManager.group = ScriptableObject.CreateInstance<EnemyGroup>();
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("group"), BattleManager.group);
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("goods"), ShopManager.goods);
    }

    private void GenerateMap()
    {
        Map.Map.map = MapGenerator.instance.GetMap(Globals.instance.seed);
        generated = true;
    }
}