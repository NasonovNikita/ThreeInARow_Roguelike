using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "PlainShield", menuName = "Items/PlainShield")]
    public class PlainShield : Item
    {
        [SerializeField] private float value;
        
        public override void Use(Unit unitBelong)
        {
            unitBelong.hp.AddMod(new Modifier(-1, ModType.Mul, value), ModAffect.ValueSub);
        }
    }
}