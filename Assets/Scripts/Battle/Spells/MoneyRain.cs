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
            LogUsage();

            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, manager.enemies.Count);
                while (manager.enemies[index] == null) index = Random.Range(0, manager.enemies.Count);
                manager.enemies[index].DoDamage(dmg);
                PToEDamageLog.Log(manager.enemies[index], manager.player, dmg);
            }
        }

        public override string Title => "Money Rain";

        public override string Description => $"Coins fall and deal {(int) value} damage each";
    }
}