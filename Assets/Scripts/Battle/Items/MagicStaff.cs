using Battle.Units;
using Battle.Units.Modifiers.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MagicStaff", menuName = "Items/MagicStaff")]
    public class MagicStaff : Item
    {
        [SerializeField] private float value;

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Tools.Percents(value));

        public override void Get()
        {
            Player.data.mana.AddWastingMod(new ManaWastingMoveMod());
            base.Get();
        }
    }
}