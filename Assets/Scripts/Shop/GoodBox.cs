using TMPro;
using UnityEngine;
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
                button.onClick.AddListener(good.Buy);
                button.onClick.AddListener(OnBuy);
                button.GetComponentInChildren<TMP_Text>().text = $"{good.GetName()} {good.price}";
            }
            catch
            {
                button.GetComponentInChildren<TMP_Text>().text = "Sorry, we are out";
            }
        }

        private void OnBuy()
        {
            if (!good.bought) return;
            button.GetComponentInChildren<TMP_Text>().text = "Sorry, we are out";
            button.onClick.RemoveAllListeners();
        }
    }
}