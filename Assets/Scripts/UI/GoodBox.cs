using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GoodBox : MonoBehaviour
    {
        public Good good;
        [SerializeField] private Button button;

        public void Start()
        {
            try
            {
                button.onClick.AddListener(good.Buy);
                button.onClick.AddListener(OnBuy);
                button.GetComponentInChildren<TMP_Text>().text = $"{good.GetName()} {good.price}";
            }
            catch
            {
                button.GetComponentInChildren<TMP_Text>().text = "Распродано";
            }
        }

        private void OnBuy()
        {
            if (!good.bought) return;
            button.GetComponentInChildren<TMP_Text>().text = "Распродано";
            button.onClick.RemoveAllListeners();
        }
    }
}