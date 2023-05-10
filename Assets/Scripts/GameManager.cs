using System.Collections.Generic;
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
    }

    
    
    public void NewGame()
    {
        Debug.unityLogger.Log("NewGame");
        
        Resources.Load<Stats>("RuntimeData/PlayerStats").Reset();
        SceneManager.LoadScene("Map");
        ResetAll();
        
    }

    public void Continue()
    {
        Debug.unityLogger.Log("Continue");
        
        if (PlayerPrefs.HasKey("vertex"))
        {
            Map.currentVertex = PlayerPrefs.GetInt("vertex");
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
        PlayerPrefs.SetInt("vertex", Map.currentVertex);
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
    }

    public void ResetAll()
    {
        Map.currentVertex = -1;
        
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