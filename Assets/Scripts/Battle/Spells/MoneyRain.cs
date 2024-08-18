using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MoneyRain", menuName = "Spells/MoneyRain")]
    public class MoneyRain : Spell
    {
        [SerializeField] private int damage;

        public override bool CantCast =>
            BattleFlowManager.CurrentlyTurningUnit is not Player ||
            Player.Data.money < useCost;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, damage);

        protected override void Waste()
        {
            Player.Data.money -= useCost;
        }

        protected override void Action()
        {
            for (var i = 0; i < useCost; i++)
            {
                var index = Random.Range(0, UnitBelong.Enemies.Count);
                while (UnitBelong.Enemies[index] == null)
                    index = Random.Range(0, UnitBelong.Enemies.Count);
                UnitBelong.Enemies[index]
                    .TakeDamage(UnitBelong.damage.ApplyDamage(damage));
            }
        }
    }
}