using Battle.Modifiers;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MagicStaff", menuName = "Items/MagicStaff")]
    public class MagicStaff : Item
    {
        [SerializeField] private float value;
        public override void Use(Unit unitBelong) {}

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Tools.Percents(value));
        
        public override void OnGet()
        {
            Player.data.AddManaMod(new Modifier(-1, ModType.Mul, ModClass.ManaWaste, value: -value));
        }
    }
}