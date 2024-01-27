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

        public override string Title => titleKeyRef.Value;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, count, Other.Tools.Percents(chance), (int)value);
    }
}