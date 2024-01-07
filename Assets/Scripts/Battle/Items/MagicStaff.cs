using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MagicStaff", menuName = "Items/MagicStaff")]
    public class MagicStaff : Item
    {
        [SerializeField] private float value;
        public override void Use(Unit unitBelong) {}

        public override void OnGet()
        {
            Player.data.mana.AddMod(new Modifier(-1, ModType.Mul, ModClass.ManaWaste, value: -value));
        }
    }
}