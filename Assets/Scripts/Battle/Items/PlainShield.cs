using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "PlainShield", menuName = "Items/PlainShield")]
    public class PlainShield : Item
    {
        [SerializeField] private int value;
        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));

        public override void OnGet()
        {
            Player.data.hp.onTakingDamageMods.Add(new HpDamageConstMod(-value, true));
        }
    }
}