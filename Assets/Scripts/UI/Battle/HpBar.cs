using Battle.Units.Stats;

namespace UI.Battle
{
    public class HpBar : StatBar
    {
        protected override Stat stat => unit.hp;
    }
}