using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "PlainShield", menuName = "Items/PlainShield")]
    public class PlainShield : Item
    {
        [SerializeField] private float value;

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));

        public override void OnGet()
        {
            Player.data.AddHpMod(new Modifier(-1, ModType.Mul, ModClass.HpDamageBase, value: -value));
        }

        public override void Use(Unit unitBelong) {}
    }
}