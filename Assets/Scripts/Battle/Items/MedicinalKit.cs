using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MedicalKit", menuName = "Items/MedicalKit")]
    public class MedicinalKit : Item
    {
        [SerializeField] private int value;

        
        public override string Title => titleKeyRef.Value;
        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));

        public override void Get()
        {
            Player.data.hp.AddHealingMod(new HealingMod(value, true));
            base.Get();
        }
    }
}