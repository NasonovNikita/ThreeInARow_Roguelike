using System;
using UI;
using UI.MessageWindows;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shop
{
    public class GoodBox : MonoBehaviour
    {
        public Good good;
        [SerializeField] private Button button;

        public void Start()
        {
            try
            {
                button.onClick.AddListener(() => good.TryBuy(OnBuy));
                button.GetComponentInChildren<Text>().text = $"{good.target.Title} {good.price}";
                button.AddComponent<ObjectWithInfo>().text = good.target.Description;
            }
            catch (Exception e)
            {
                button.GetComponentInChildren<Text>().text = "Sorry, we are out";
                DevDebugWindow.instance.Write("A bug occured. Tell me, if it actually broke something. " +
                                              $"Thrown error: {e.Message}");
            }
        }

        private void OnBuy()
        {
            button.GetComponentInChildren<Text>().text = "Sorry, we are out";
            button.onClick.RemoveAllListeners();
        }
    }
}