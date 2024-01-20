using Battle.BattleEventHandlers;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "HotBlood", menuName = "Spells/HotBlood")]
    public class HotBlood : Spell
    {
        [SerializeField] private int chance;
        
        public override void Cast()
        {
            if (CantCast()) return;

            unitBelong.mana.Waste(useCost);
            
            LogUsage();
            unitBelong.AddDamageMod(new Modifier(-1, ModType.Add,
                ModClass.DamageTypedStat, value: value));
            unitBelong.AddMod(new Modifier(count, ModType.Ignition));
            new RandomIgnitionEvent(chance, unitBelong, count);
        }

        public override string Title => "Hot Blood";

        public override string Description =>
            $"You may start burning at any of next {count} turns wih chance {Other.Tools.Percents(chance)}% each." +
            $"Gain {(int)value} physical dmg (per gem)";
    }
}