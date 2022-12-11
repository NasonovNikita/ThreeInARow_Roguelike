using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider _slider;
    
    [SerializeField]
    private Unit unit;

    [SerializeField]
    private TMP_Text text;

    private void Start()
    {
        if (unit.BaseMana == 0)
        {
            Destroy(gameObject);
        }
        
        _slider = GetComponent<Slider>();
        _slider.maxValue = unit.BaseMana;
        _slider.value = unit.Mana;
    }

    private void Update()
    {
        _slider.value = unit.Mana;
        text.text = $"{unit.Mana}/{unit.BaseMana}";
    }
}