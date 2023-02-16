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
    private GameObject[] messages;

    [SerializeField]
    private Stats playerStats;

    private void Awake()
    {
        battleManager.gameManager = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
                playerStats.Reset();
            #else
                Application.Quit();
                playerStats.Reset();
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

    private void InstantiateEndMenu(GameObject message)
    {
        GameObject menu = Instantiate(message, canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(Restart);
        buttons[1].onClick.AddListener(Exit);
        menu.gameObject.SetActive(true);
    }

    public void SavePlayerStats(Player player)
    {
        playerStats.playerHp = player.Hp;
        playerStats.playerMana = player.Mana;
    }

    public void LoadPlayerStats(Player player)
    {
        player.SetHp(playerStats.playerHp);
        player.SetMana(playerStats.playerMana);
    }
}