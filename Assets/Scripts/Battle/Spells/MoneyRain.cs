using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MoneyRain", menuName = "Spells/MoneyRain")]
    public class MoneyRain : Spell
    {
        protected override bool CantCast => NotAllowedTurn || Player.data.money < useCost;

        protected override void Waste() => Player.data.money -= useCost;

        protected override void Action()
        {
            Damage dmg = new Damage(phDmg: (int) value);

            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, manager.Enemies.Count);
                while (manager.Enemies[index] == null) index = Random.Range(0, manager.Enemies.Count);
                manager.Enemies[index].TakeDamage(dmg);
            }
        }

        public override string Description => string.Format(descriptionKeyRef.Value, (int)value);
    }
}