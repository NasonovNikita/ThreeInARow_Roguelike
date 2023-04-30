using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.NewGame();
    }

    public void Continue()
    {
        GameManager.Continue();
    }

    public void Exit()
    {
        GameManager.Exit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}