namespace Battle.Modifiers
{
    /// <summary>
    ///     IModifierAble object must have a value, that is supposed to be modified soon
    ///     and a ModifierList (smart list of Modifiers)
    /// </summary>
    public interface IModifierAble
    {
        public ModifierList Modifiers { get; }

        public int Value { get; }
    }
}