using System;
using Battle.Units;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    [Serializable]
    public class Item : ScriptableObject
    {
        [SerializeField] public string title;
        [SerializeField] private Condition cond;

        public void Init(Unit unit)
        {
            cond.Init(unit);
        }
    }
}