using System.Collections;
using System.Linq;
using Battle.Spells;
using Other;

namespace Battle.Units.AI
{
    public class SpellUserAi : BasicAi
    {
        private Spell ChooseSpell =>
            Tools.Random.RandomChoose(
                attachedEnemy.spells.Where(spell => !spell.CantCast));

        public override IEnumerator Act()
        {
            yield return StartCoroutine(UseSpells());

            yield return base.Act();
        }

        private IEnumerator UseSpells()
        {
            if (attachedEnemy.spells.Any(spell => !spell.CantCast))
                yield return StartCoroutine(ChooseSpell.Cast());
        }
    }
}