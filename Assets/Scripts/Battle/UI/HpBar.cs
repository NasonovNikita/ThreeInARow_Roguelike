using Battle.Units.Stats;

namespace Battle.UI
{
    public class HpBar : StatBar
    {
        protected override Stat Stat => unit.hp;
    }
}