using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "PlainShield", menuName = "Items/PlainShield")]
    public class PlainShield : Item
    {
        [SerializeField] private float value;
        
        public override void Use(Unit unitBelong)
        {
            unitBelong.AddHpMod(new DamageMod(-1, ModType.Mul, value: value));
        }
    }
}