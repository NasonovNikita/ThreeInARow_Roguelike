using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private Slider _slider;
    [SerializeField]
    private Unit unit;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = unit.BaseHp;
        _slider.value = unit.Hp;
    }

    private void Update()
    {
        _slider.value = unit.Hp;
    }
    
}
