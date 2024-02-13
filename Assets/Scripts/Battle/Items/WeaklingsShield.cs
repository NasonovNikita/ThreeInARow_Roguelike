using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "Weakling'sShield", menuName = "Items/Weakling'sShield")]
    public class WeaklingsShield : Item
    {
        [SerializeField] private int lostDamage;
        [SerializeField] private int notGottenDamage;
        public override void Use(Unit unitBelong) {}

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, lostDamage, notGottenDamage);

        public override void OnGet()
        {
            Player.data.damage.phDmg.ChangeBorderUp(-lostDamage, -lostDamage);
            Player.data.AddDamageMod(new Modifier(-1, ModType.Add, ModClass.DamageTypedStat,
                DmgType.Physic, false, -lostDamage, always: true));
            Player.data.AddHpMod(new Modifier(-1, ModType.Add,
                ModClass.HpDamageBase, value: -notGottenDamage, always: true));
        }
    }
}