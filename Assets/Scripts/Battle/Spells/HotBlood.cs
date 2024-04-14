using Battle.Modifiers.StatModifiers;
using Battle.Modifiers.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "HotBlood", menuName = "Spells/HotBlood")]
    public class HotBlood : Spell
    {
        [SerializeField] private int chance;
        [SerializeField] private int moves;
        [SerializeField] private int burningMoves;
        [SerializeField] private int damage;

        protected override void Action()
        {
            unitBelong.AddStatus(new RandomIgnition(chance, moves, burningMoves));
            unitBelong.damage.mods.Add(new DamageConstMod(damage));
        }

        public override string Description =>
            string.Format(descriptionKeyRef.Value, damage, moves, chance);
    }
}