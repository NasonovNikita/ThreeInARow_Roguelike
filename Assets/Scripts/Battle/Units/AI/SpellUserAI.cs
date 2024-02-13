using System.Collections;
using System.Linq;
using Battle.Match3;
using UnityEngine;

namespace Battle.Units.AI
{
    public class SpellUserAI : BaseEnemyAI
    {
        [SerializeField] private float manaProfit;

        public override IEnumerator Act()
        {
            yield return StartCoroutine(UseSpells());
            yield return StartCoroutine(base.Act());
        }

        protected override int CountProfit(Gem[,] box)
        {
            return (int)(base.CountProfit(box) +
                         CountMoveResults(box)[GemType.Mana] * attachedEnemy.manaPerGem * manaProfit);
        }

        protected virtual IEnumerator UseSpells()
        {
            if (attachedEnemy.spells.Count == 0) yield break;

            var possibleSpells =
                attachedEnemy.spells.Where(spell => attachedEnemy.mana >= spell.useCost).ToList();

            if (possibleSpells.Count == 0) yield break;
            var chosenSpell = possibleSpells[Random.Range(0, possibleSpells.Count)];
            yield return StartCoroutine(chosenSpell.Cast());
        }
    }
}