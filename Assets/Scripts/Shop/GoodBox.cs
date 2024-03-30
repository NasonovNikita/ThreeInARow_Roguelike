using System;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

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
                var info = button.GetComponent<InfoObject>();
                info.text = good.target.Description;
            }
            catch (Exception)
            {
                button.GetComponentInChildren<Text>().text = "Sorry, we are out";
            }
        }

        private void OnBuy()
        {
            button.GetComponentInChildren<Text>().text = "Sorry, we are out";
            button.onClick.RemoveAllListeners();
        }
    }
}