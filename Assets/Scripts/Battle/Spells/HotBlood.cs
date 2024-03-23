using Battle.BattleEventHandlers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "HotBlood", menuName = "Spells/HotBlood")]
    public class HotBlood : Spell
    {
        [SerializeField] private int chance;

        protected override void Action()
        {
            unitBelong.AddDamageMod(new MoveStatModifier(-1, ModType.Add,
                ModClass.DamageTypedStat, value: value));
            unitBelong.AddMod(new MoveStatModifier(count, ModType.Ignition));
            new RandomIgnitionEvent(chance, unitBelong, count);
        }

        public override string Description =>
            string.Format(descriptionKeyRef.Value, (int)value, count, chance);
    }
}