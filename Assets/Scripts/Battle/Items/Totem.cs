using Battle.Units;
using Battle.Units.Statuses;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "Totem", menuName = "Items/Totem")]
    public class Totem : Item
    {
        [SerializeField] private int chance;

        public override void OnGet()
        {
            Player.data.AddStatus(new Immortality(chance, true));
        }
    }
}