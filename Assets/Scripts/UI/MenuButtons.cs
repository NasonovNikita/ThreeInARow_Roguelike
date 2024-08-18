using Core;
using Core.Singleton;
using Other;
using UnityEngine;

namespace UI
{
    public class MenuButtons : MonoBehaviour
    {
        public void NewGame()
        {
            GameManager.Instance.NewGame();
        }

        public void Continue()
        {
            GameManager.Instance.ContinueGameRun();
        }

        public void Exit()
        {
            GameManager.Instance.Exit();
        }

        public void MainMenu()
        {
            GameManager.Instance.MainMenu();
        }

        public void Settings()
        {
            GameManager.Instance.GoToSettings();
        }

        public void EnterMap()
        {
            GameManager.Instance.GoToMap();
        }

        public void ShowInventory()
        {
            Tools.InstantiateUI(PrefabsContainer.Instance.inventoryManager);
        }

        public void ChooseLanguage()
        {
            Tools.InstantiateUI(PrefabsContainer.Instance.languageChooser);
        }
    }
}