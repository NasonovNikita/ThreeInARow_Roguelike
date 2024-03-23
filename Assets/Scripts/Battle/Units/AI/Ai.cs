using System.Collections;
using UnityEngine;

namespace Battle.Units.AI
{
    public abstract class Ai : MonoBehaviour
    {
        [SerializeField] protected Enemy attachedEnemy;

        public abstract IEnumerator Act();
    }
}