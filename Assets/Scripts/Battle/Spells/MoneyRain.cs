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
            Damage dmg = new Damage(phDmg: (int) value);

            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, manager.enemies.Count);
                manager.enemies[index].DoDamage(dmg);
                PToEDamageLog.Log(manager.enemies[index], manager.player, dmg);
            }
        }
    }
}