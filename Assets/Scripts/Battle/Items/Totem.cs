using Battle.BattleEventHandlers;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Items
{
    public class Totem : Item
    {
        [SerializeField] private int chance;
        [SerializeField] private int leftHp;
        public override void Use(Unit unitBelong)
        {
            new PlayerGettingHitThen(() =>
            {
                if (unitBelong.hp != 0) return;

                if (Tools.Random.RandomChance(chance))
                {
                    unitBelong.hp.Heal(leftHp);
                }
            });
        }
    }
}