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

        protected override void Waste() => Player.data.money -= useCost;

        protected override void Action()
        {
            for (int i = 0; i < useCost; i++)
            {
                int index = Random.Range(0, unitBelong.Enemies.Count);
                while (unitBelong.Enemies[index] == null) index = Random.Range(0, unitBelong.Enemies.Count);
                unitBelong.Enemies[index].TakeDamage(damage);
            }
        }

        public override string Description => string.Format(descriptionKeyRef.Value, damage);
    }
}