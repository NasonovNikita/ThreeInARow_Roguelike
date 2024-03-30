namespace Battle.Modifiers.StatModifiers
{
    public abstract class SimpleStatModifier : BaseStatModifier
    {
        public override string SubInfo => value.ToString();

        public override bool ToDelete => value == 0;

        protected SimpleStatModifier(int value, bool save = false) : base(value, save) {}
    }
}