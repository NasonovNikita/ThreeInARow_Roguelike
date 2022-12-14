using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    
    [SerializeField]
    private BattleManager battleManager;

    [SerializeField]
    private Menu[] messages;

    private void Awake()
    {
        battleManager.gameManager = this;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void exit()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        }

    public void Win()
    {
        InstantiateEndMenu(messages[0]);
    }

    public void Lose()
    {
        InstantiateEndMenu(messages[1]);
    }

    private void InstantiateEndMenu(Menu message)
    {
        Menu menu = Instantiate(message, canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(restart);
        buttons[1].onClick.AddListener(exit);
        menu.gameObject.SetActive(true);
    }
}
