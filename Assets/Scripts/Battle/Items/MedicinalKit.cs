using Battle.Units;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MedicalKit", menuName = "Items/MedicalKit")]
    public class MedicinalKit : Item
    {
        [SerializeField] private int value;
        public override string Description => descriptionKeyRef.Value.IndexErrorProtectedFormat(value);

        public override void OnGet()
        {
            Player.data.hp.onHealingMods.Add(new HealingConstMod(value, true));
        }
    }
}