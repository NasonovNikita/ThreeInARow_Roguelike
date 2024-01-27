using Battle;
using Battle.Modifiers;
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
        [SerializeField] private KnotTextKeyReference element;
        private Text text;
        
        public void Awake()
        {
            text = GetComponent<Text>();
        }

        public void Update()
        {
            text.text =
                $"{hp.Value}/{mana.Value}: {Player.data.hp.value}/{Player.data.mana.value}\n" +
                $"{cash.Value}: {Player.data.money}\n" +
                (Player.data.chosenElement != DmgType.Physic
                    ? $"{element.Value}: {DescriptionKeys.instance.DmgType(Player.data.chosenElement)}"
                    : "");
        }
    }
}
