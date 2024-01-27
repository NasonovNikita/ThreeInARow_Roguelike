using System.Collections.Generic;
using Battle;
using Battle.Units;
using Knot.Localization;
using UI.MessageWindows;
using UnityEngine;

namespace UI
{
    public class ElementChooser : ObjectWithInfo
    {
        private const int WastingMana = 30;
        [SerializeField] private DmgType element;

        protected override string Text => ElementsDescriptions[element];

        private static Dictionary<DmgType, string> ElementsDescriptions => new()
        {
            { DmgType.Fire, string.Format(KnotLocalization.GetText("FireDescription"),
                ElementsProperties.FireDamage, 1 + ElementsProperties.FiredDamageModVal)},
            
            { DmgType.Cold, string.Format(KnotLocalization.GetText("ColdDescription"),
                ElementsProperties.MissingOnFreezeChance, Other.Tools.Percents(ElementsProperties.ColdDamageLoss))},
            
            { DmgType.Poison, string.Format(KnotLocalization.GetText("PoisonDescription"),
                ElementsProperties.PoisonDamage)},
            
            { DmgType.Light, string.Format(KnotLocalization.GetText("LightDescription"),
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