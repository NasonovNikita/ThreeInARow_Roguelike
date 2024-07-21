using Battle.Units.Stats;

namespace Battle.UI
{
    public class ManaBar : StatBar
    {
        protected override Stat Stat => unit.mana;
    }
}