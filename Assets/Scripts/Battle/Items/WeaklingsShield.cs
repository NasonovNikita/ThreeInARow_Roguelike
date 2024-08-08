using Battle.Units;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "Weakling'sShield", menuName = "Items/Weakling'sShield")]
    public class WeaklingsShield : Item
    {
        [SerializeField] private int lostDamage;
        [SerializeField] private int notGottenDamage;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(lostDamage, notGottenDamage);

        public override void OnGet()
        {
            Player.Data.damage.mods.Add(new DamageConstMod(-lostDamage, true));
            Player.Data.hp.onTakingDamageMods.Add(new HpDamageConstMod(-notGottenDamage, true));
        }
    }
}