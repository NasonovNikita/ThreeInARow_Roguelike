using Battle.Modifiers;

namespace Battle.Grid.Modifiers
{
    public interface IModifierAble
    {
        public ModifierList Modifiers { get; }

        public int Value { get; }
    }
}