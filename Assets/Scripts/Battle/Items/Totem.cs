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
                if (unitBelong.Hp != 0) return;

                if (Tools.RandomChance(chance))
                {
                    unitBelong.Hp.Heal(leftHp);
                }
            });
        }
    }
}