using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private GameManager gm;
    public void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void NewGame()
    {
        gm.NewGame();
    }

    public void Continue()
    {
        gm.Continue();
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