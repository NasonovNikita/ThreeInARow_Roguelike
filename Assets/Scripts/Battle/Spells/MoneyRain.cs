using System.Collections;
using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MoneyRain", menuName = "Spells/MoneyRain")]
    public class MoneyRain : Spell
    {
        public override IEnumerator Cast()
        {
            if (Player.data.money < useCost) yield break;
            Player.data.money -= useCost;
            Damage dmg = new Damage(phDmg: (int) value);
            LogUsage();

            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, manager.enemies.Count);
                while (manager.enemies[index] == null) index = Random.Range(0, manager.enemies.Count);
                manager.enemies[index].TakeDamage(dmg);
                //PToEDamageLog.Log(manager.enemies[index], manager.player, dmg);

                yield return Wait();
            }
        }

        public override string Description => string.Format(descriptionKeyRef.Value, (int)value);
    }
}