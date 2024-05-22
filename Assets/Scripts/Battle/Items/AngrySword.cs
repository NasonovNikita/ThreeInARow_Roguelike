using Battle.Modifiers.Statuses;
using Battle.Units;
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
            string.Format(descriptionKeyRef.Value, hpLessThen, bonus);

        public override void OnGet()
        {
            Player.data.AddStatus(new Fury(bonus, hpLessThen, true));
        }
    }
}