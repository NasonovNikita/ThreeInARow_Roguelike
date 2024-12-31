using System.Collections;
using UnityEngine;

namespace Battle.Units.AI
{
    /// <summary>
    ///     Enemy's AI. Is used to describe Enemy's actions using inheritance.
    ///     <seealso cref="BasicAi"/>
    ///     <seealso cref="SpellUserAi"/>
    /// </summary>
    public abstract class Ai : MonoBehaviour
    {
        [SerializeField] protected Enemy attachedEnemy;

        /// <summary>
        ///     Enemy's act during battle.
        /// </summary>
        public abstract IEnumerator Act();
    }
}