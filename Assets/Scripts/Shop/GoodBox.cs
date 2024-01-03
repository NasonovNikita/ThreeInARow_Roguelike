using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shop
{
    public class GoodBox : MonoBehaviour, IPointerClickHandler
    {
        public Good good;
        [SerializeField] private Button button;

        public void Start()
        {
            try
            {
                button.onClick.AddListener(good.Buy);
                button.onClick.AddListener(OnBuy);
                button.GetComponentInChildren<Text>().text = $"{good.Title} {good.price}";
                DevDebugAbleObject a = button.AddComponent<DevDebugAbleObject>();
                a.text = good.Description;
            }
            catch (Exception e)
            {
                button.GetComponentInChildren<Text>().text = "Sorry, we are out";
                DevDebugWindow.instance.Write("A bug occured. Tell me, if it actually broke something." +
                                              $"Thrown error: {e.Message}");
            }
        }

        private void OnBuy()
        {
            if (!good.bought) return;
            button.GetComponentInChildren<Text>().text = "Sorry, we are out";
            button.onClick.RemoveAllListeners();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                DevDebugWindow.instance.Write(good.Description);
            }
        }
    }
}