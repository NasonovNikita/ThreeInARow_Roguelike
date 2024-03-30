using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "Weakling'sShield", menuName = "Items/Weakling'sShield")]
    public class WeaklingsShield : Item
    {
        [SerializeField] private int lostDamage;
        [SerializeField] private int notGottenDamage;

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, lostDamage, notGottenDamage);

        public override void Get()
        {
            Player.data.damage.AddMod(new DamageMod(-lostDamage, true));
            Player.data.hp.AddDamageMod(new DamageMod(-notGottenDamage, true));
            
            base.Get();
        }
    }
}