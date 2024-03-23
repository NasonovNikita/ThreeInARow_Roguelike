using Battle.Units;
using Battle.Units.Modifiers.StatModifiers;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(menuName = "Items/Mace")]
    public class Mace : Item
    {
        [SerializeField] private int moves;
        [SerializeField] private int addition;


        public override string Title => titleKeyRef.Value;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, addition, moves);

        public override void Get()
        {
            Enemy.OnGettingHit += enemy =>
                enemy.hp.AddDamageMod(new DamageMoveMod(addition, -1));
            base.Get();
        }
    }
}