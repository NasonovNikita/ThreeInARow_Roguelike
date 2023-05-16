using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public void NewGame()
    {
        GameManager.instance.NewGame();
    }

    public void Continue()
    {
        GameManager.instance.Continue();
    }
    public void Exit()
    {
        GameManager.instance.Exit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}