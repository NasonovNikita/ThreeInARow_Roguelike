#if UNITY_EDITOR


using UnityEngine;
using UnityEngine.SceneManagement;

public static class BackToMainMenu
{
    private static bool _returned;
    
    [RuntimeInitializeOnLoadMethod]
    public static void MainMenu()
    {
        if (!_returned && SceneManager.GetActiveScene().name != "MainMenu")
        {
            //SceneManager.LoadScene("MainMenu");
        }

        _returned = true;
    }
}

#endif