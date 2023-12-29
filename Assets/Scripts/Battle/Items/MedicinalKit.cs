using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Items
{
    public class MedicinalKit : Item
    {
        [SerializeField] private float value;
        public override void Use(Unit unitBelong)
        {
            unitBelong.AddHpMod(new HealingMod(-1, ModType.Mul, true, value));
        }
    }
}