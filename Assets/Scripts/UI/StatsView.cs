using Battle.Units;
using Knot.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class StatsView : MonoBehaviour
    {
        [SerializeField] private KnotTextKeyReference hp;
        [SerializeField] private KnotTextKeyReference mana;
        [SerializeField] private KnotTextKeyReference cash;
        private Text text;

        public void Awake()
        {
            text = GetComponent<Text>();
        }

        public void Update()
        {
            text.text =
                $"{hp.Value}/{mana.Value}: {Player.Data.hp.Value}/{Player.Data.mana.Value}\n" +
                $"{cash.Value}: {Player.Data.money}\n";
        }
    }
}