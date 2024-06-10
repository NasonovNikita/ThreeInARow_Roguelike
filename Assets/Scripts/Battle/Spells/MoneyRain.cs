using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MoneyRain", menuName = "Spells/MoneyRain")]
    public class MoneyRain : Spell
    {
        [SerializeField] private int damage;

        public override bool CantCast =>
            battleFlowManager.CurrentlyTurningUnit is not Player ||
            Player.data.money < useCost;

        public override string Description => string.Format(descriptionKeyRef.Value, damage);

        protected override void Waste()
        {
            Player.data.money -= useCost;
        }

        protected override void Action()
        {
            for (var i = 0; i < useCost; i++)
            {
                var index = Random.Range(0, unitBelong.Enemies.Count);
                while (unitBelong.Enemies[index] == null) index = Random.Range(0, unitBelong.Enemies.Count);
                unitBelong.Enemies[index].TakeDamage(unitBelong.damage.ApplyDamage(damage));
            }
        }
    }
}