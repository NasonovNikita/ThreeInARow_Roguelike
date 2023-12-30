using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "Weakling'sShield", menuName = "Items/Weakling'sShield")]
    public class WeaklingsShield : Item
    {
        [SerializeField] private int lostDamage;
        [SerializeField] private int notGottenDamage;
        public override void Use(Unit unitBelong)
        {
            unitBelong.AddDamageMod(new DamageMod(-1, ModType.Add, false, -lostDamage));
            unitBelong.AddHpMod(new DamageMod(-1, ModType.Add, value: -notGottenDamage));
        }
    }
}