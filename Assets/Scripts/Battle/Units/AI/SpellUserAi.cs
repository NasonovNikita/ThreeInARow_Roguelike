using System.Collections;
using System.Linq;
using Battle.Spells;

namespace Battle.Units.AI
{
    public class SpellUserAi : BasicAi
    {
        private Spell ChooseSpell =>
            Other.Tools.Random.RandomChoose(attachedEnemy.spells.Where(spell => !spell.CantCast));

        public override IEnumerator Act()
        {
            yield return StartCoroutine(ChooseSpell.Cast());
            
            yield return base.Act();
        }
    }
}