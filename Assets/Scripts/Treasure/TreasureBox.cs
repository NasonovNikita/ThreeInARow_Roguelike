using Other;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Treasure
{
    public class TreasureBox : MonoBehaviour
    {
        public GetAble treasure;
        [SerializeField] private Button button;

        public void Start()
        {
            button.onClick.AddListener(treasure.Get);
            button.onClick.AddListener(OnGet);
            button.GetComponentInChildren<Text>().text = treasure.Title;
            button.AddComponent<DevDebugAbleObject>().text = treasure.Description;
        }

        private void OnGet()
        {
            button.GetComponentInChildren<Text>().text = "got";
            button.onClick.RemoveAllListeners();
        }
    }
}