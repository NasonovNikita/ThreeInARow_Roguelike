using Battle.Modifiers.StatModifiers;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MagicStaff", menuName = "Items/MagicStaff")]
    public class MagicStaff : Item
    {
        [SerializeField] private int value;

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Tools.Percents(value));

        public override void Get()
        {
            Player.data.mana.AddWastingMod(new ManaWastingMod(value, true));
            base.Get();
        }
    }
}