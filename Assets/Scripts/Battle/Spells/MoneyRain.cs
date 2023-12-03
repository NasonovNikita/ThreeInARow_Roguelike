using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MoneyRain", menuName = "Spells/MoneyRain")]
    public class MoneyRain : Spell
    {
        public override void Cast()
        {
            if (Player.data.money < useCost) return;

            Player.data.money -= useCost;
            for (int i = 0; i < count; i++)
            {
                manager.enemies[Random.Range(0, manager.enemies.Count)].DoDamage(new Damage(phDmg: (int) value));
            }
        }
    }
}