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
            unitBelong.AddHpMod(new Modifier(-1, ModType.Mul, ModClass.HpHealing, value: value));
        }

        public override string Title => "Medical Kit";

        public override string Description => $"{Other.Tools.Percents(value)}% more healing";
    }
}