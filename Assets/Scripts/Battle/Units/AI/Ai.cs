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

        // TODO use with BattleFlowManager.EndedProcesses .
        // TODO Maybe also create object for those processes to launch as Coroutine.
        /// <summary>
        ///     Enemy's act during battle.
        /// </summary>
        public abstract IEnumerator Act();
    }
}