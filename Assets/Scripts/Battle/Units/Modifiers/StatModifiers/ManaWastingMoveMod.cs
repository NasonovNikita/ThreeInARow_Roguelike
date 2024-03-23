using UnityEngine;

namespace Battle.Units.Modifiers.StatModifiers
{
    public class ManaWastingMoveMod : MoveCounter, IStatModifier
    {
        private readonly int value;
        
        public ManaWastingMoveMod(int value, int moves, bool delay = false, bool permanent = false) :
            base(moves, delay, permanent) =>
            this.value = value;

        public Sprite Sprite => throw new System.NotImplementedException();

        public string Description => throw new System.NotImplementedException();

        bool IModifier.ConcatAble(IModifier other) =>
            other is ManaWastingMoveMod same && same.value == value && BothPermanent(other);

        int IStatModifier.Modify(int val) => val + value;
    }
}