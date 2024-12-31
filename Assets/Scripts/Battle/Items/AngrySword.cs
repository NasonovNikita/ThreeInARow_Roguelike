using Battle.Units;
using Battle.Units.Statuses;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "AngrySword", menuName = "Items/AngrySword")]
    public class AngrySword : Item
    {
        [SerializeField] private int hpLessThen;
        [SerializeField] private int bonus;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(hpLessThen, bonus);

        public override void OnGet()
        {
            Player.Data.AddStatus(new Fury(bonus, hpLessThen, true));
        }
    }
}