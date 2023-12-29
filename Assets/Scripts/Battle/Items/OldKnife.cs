using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "OldKnife", menuName = "Items/OldKnife")]
    public class OldKnife : Item
    {
        [SerializeField] private int value;
        public override void Use(Unit unitBelong)
        {
            unitBelong.AddDamageMod(new DamageMod(-1, ModType.Add, value: value));
        }
    }
}