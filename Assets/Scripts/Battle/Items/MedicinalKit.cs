using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MedicalKit", menuName = "Items/MedicalKit")]
    public class MedicinalKit : Item
    {
        [SerializeField] private float value;
        public override void Use(Unit unitBelong)
        {
            unitBelong.AddHpMod(new Modifier(-1, ModType.Mul, ModClass.HpHealing,
                value: value, always: true));
        }

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}