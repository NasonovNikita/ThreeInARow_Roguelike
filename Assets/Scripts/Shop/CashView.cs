using Battle.Units;
using Knot.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    [RequireComponent(typeof(Text))]
    public class CashView : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private KnotTextKeyReference cashTextRef;

    
        public void Update()
        {
            text.text = $"{cashTextRef.Value}: {Player.data.money}";
        }
    }
}