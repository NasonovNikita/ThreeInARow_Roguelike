using Battle.Units.StatModifiers;
using Battle.Units.Statuses;
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

        public override string Description =>
            string.Format(descriptionKeyRef.Value, damage, moves, chance);

        protected override void Action()
        {
            unitBelong.Statuses.Add(new RandomIgnition(chance, moves, burningMoves));
            unitBelong.damage.mods.Add(new DamageConstMod(damage));
        }
    }
}