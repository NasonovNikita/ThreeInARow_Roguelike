using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "Weakling'sShield", menuName = "Items/Weakling'sShield")]
    public class WeaklingsShield : Item
    {
        [SerializeField] private int lostDamage;
        [SerializeField] private int notGottenDamage;
        public override void Use(Unit unitBelong) {}

        public override void OnBuy()
        {
            Player.data.damage.phDmg.ChangeBorderUp(-lostDamage, -lostDamage);
            Player.data.unitHp.AddMod(new Modifier(-1, ModType.Add, ModClass.DamageBase, value: -notGottenDamage));
        }
    }
}