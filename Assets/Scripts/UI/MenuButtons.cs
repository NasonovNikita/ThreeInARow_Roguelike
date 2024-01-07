using Core;
using UnityEngine;

namespace UI
{
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
            GameManager.instance.MainMenu();
        }

        public void Settings()
        {
            GameManager.instance.Settings();
        }

        public void EnterMap()
        {
            GameManager.instance.EnterMap();
        }
    }
}