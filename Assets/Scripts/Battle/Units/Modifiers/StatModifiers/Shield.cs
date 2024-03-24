using System;
using UI.Battle;
using UnityEngine;

namespace Battle.Units.Modifiers.StatModifiers
{
    [Serializable]
    public class Shield : IStatModifier, IConcatAble, IModIconAble
    {
        [SerializeField] private Counter counter;

        public Shield(int count) => counter = new Counter(count);

        public Sprite Sprite => throw new System.NotImplementedException();

        public string Tag => throw new System.NotImplementedException();

        public string Description => throw new System.NotImplementedException();
        public string SubInfo => counter.SubInfo;

        public bool ToDelete => counter.EndedWork;

        int IStatModifier.Modify(int val) => counter.Decrease(val);
        public bool ConcatAbleWith(IConcatAble other) => other is Shield;

        public void Concat(IConcatAble other)
        {
            throw new System.NotImplementedException();
        }
    }
}