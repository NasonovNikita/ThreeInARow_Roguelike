using Battle.Units;
using Battle.Units.Enemies;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
    public class TimeStop : Spell
    {
        [SerializeField] private Modifier stunMod;
        
        public override void Cast()
        {
            if (CantCast()) return;
        
            unit.mana -= manaCost;

            switch (unit)
            {
                case Player:
                {
                    foreach (Enemy enemy in BattleManager.enemies)
                    {
                        stunMod.Use(enemy);
                    }

                    break;
                }
                case Enemy:
                    stunMod.Use(BattleManager.player);
                    break;
            }
        }
    }
}