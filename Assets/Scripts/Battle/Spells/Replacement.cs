using System.Collections;
using Battle.Match3;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(menuName = "Spells/Replacement")]
    public class Replacement : Spell
    {
        protected override void Action()
        {
            var grid = manager.grid;
            for (int i = 0; i < count; i++)
            {
                grid.ReplaceGem(Random.Range(0, grid.sizeX), Random.Range(0, grid.sizeY), GemType.Mana);
            }
        }

        protected override IEnumerator Wait()
        {
            yield return unitBelong.StartCoroutine(manager.grid.Refresh());
            yield return base.Wait();
        }
    }
}