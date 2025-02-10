using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class SacrificialDagger : Match3Cell, IModifierAble
    {
        [SerializeField] private int manaAmount;
        [SerializeField] private int selfDamage;
        
        public override string Description => descriptionKeyRef.Value.FormatByKeys(
            new Dictionary<string, object>
        {
            {"manaAmount", manaAmount},
            {"selfDamage", selfDamage}
        });
        
        public override bool IsSameType(Cell other)
        {
            return other is SacrificialDagger;
        }

        protected override void Use()
        {
            Player.Instance.mana.Refill(IIntModifier.UseModList(Modifiers.List, manaAmount));
            Player.Instance.TakeDamage(selfDamage);
        }

        public ModifierList Modifiers { get; } = new();
        public int Value => manaAmount;
    }
}