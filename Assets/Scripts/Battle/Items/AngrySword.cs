using Battle.Units;
using Battle.Units.Modifiers.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "AngrySword", menuName = "Items/AngrySword")]
    public class AngrySword : Item
    {
        [SerializeField] private int hpLessThen;
        [SerializeField] private int bonus;
        
        public override string Title => titleKeyRef.Value;

        public override string Description => 
            string.Format(descriptionKeyRef.Value, hpLessThen, Tools.Percents(bonus));

        public override void Get()
        {
            Player.data.damage.AddMod(new Fury(hpLessThen, bonus, true));
            base.Get();
        }
    }
}