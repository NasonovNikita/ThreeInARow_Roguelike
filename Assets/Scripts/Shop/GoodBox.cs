using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodBox : MonoBehaviour
{
    public Good good;
    [SerializeField] private Button button;

    public void Awake()
    {
        button.onClick.AddListener(good.Buy);
        button.GetComponentInChildren<TMP_Text>().text = $"{good.GetName()} {good.price}";
    }
}