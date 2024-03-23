namespace Battle.Units.Modifiers
{
    public abstract class Modifier
    {
        protected readonly bool permanent;
        protected Unit unitBelong;

        protected Modifier(bool permanent) => this.permanent = permanent;

        public void Init(Unit unit) => unitBelong = unit;

        protected bool BothPermanent(IModifier other) => ((Modifier)other).permanent == permanent;
        

        public bool KeepBetweenBattles => permanent;
    }
}