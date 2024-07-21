using System;
using Audio;
using Core;
using Core.Saves;
using Core.Singleton;
using Map.Nodes.Managers;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class SceneManager : MonoBehaviour
    {
        public Canvas canvas;
        public static SceneManager Instance { get; private set; }


        public void Awake()
        {
            Instance = this;

            GameSave.Save();
        }

        public void Start()
        {
            AudioManager.Instance.StopAll();

            AudioManager.Instance.Play(AudioEnum.Map);

            NodeController.Instance.OnCameOutFromFinalNode += Win;

            NodeController.Instance.OnSceneEnter();

            OnSceneFullyLoaded?.Invoke();
        }

        public void OnDisable()
        {
            NodeController.Instance.OnSceneLeave();
            OnSceneLeave?.Invoke();
        }

        public static event Action OnSceneFullyLoaded;
        public static event Action OnSceneLeave;

        private void Win()
        {
            GameObject menu = Instantiate(PrefabsContainer.instance.winMessage,
                UICanvas.Instance.transform, false);
            var buttons = menu.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(GameManager.instance.NewGame);
            buttons[1].onClick.AddListener(GameManager.instance.MainMenu);
            menu.gameObject.SetActive(true);
        }
    }
}