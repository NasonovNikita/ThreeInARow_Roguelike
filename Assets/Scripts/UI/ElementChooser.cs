using System.Collections.Generic;
using Battle;
using Battle.Units;
using Knot.Localization;
using UI.MessageWindows;
using UnityEngine;

namespace UI
{
    public class ElementChooser : InfoObject
    {
        private const int WastingMana = 30;
        [SerializeField] private DmgType element;
        [SerializeField] private KnotTextKeyReference fireDescription;
        [SerializeField] private KnotTextKeyReference coldDescription;
        [SerializeField] private KnotTextKeyReference poisonDescription;
        [SerializeField] private KnotTextKeyReference lightDescription;

        protected override string Text => ElementsDescriptions[element];

        private Dictionary<DmgType, string> ElementsDescriptions => new()
        {
            { DmgType.Fire, string.Format(fireDescription.Value,
                ElementsProperties.FireDamage, 1 + ElementsProperties.FiredDamageModVal)},
            
            { DmgType.Cold, string.Format(coldDescription.Value,
                ElementsProperties.MissingOnFreezeChance, Other.Tools.Percents(ElementsProperties.ColdDamageLoss))},
            
            { DmgType.Poison, string.Format(poisonDescription.Value,
                ElementsProperties.PoisonDamage)},
            
            { DmgType.Light, string.Format(lightDescription.Value,
                ElementsProperties.LightHealRate)}
        };


        public void Choose()
        {
            if (Player.data.mana < WastingMana || Player.data.chosenElement == element) return;
            Player.data.mana.Waste(WastingMana);
            Player.data.chosenElement = element;
        }
    }
}