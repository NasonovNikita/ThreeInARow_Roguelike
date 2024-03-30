using System;
using Core;
using UI.Battle;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class Shield : IStatModifier, IConcatAble, IModIconAble
    {
        [SerializeField] private Counter counter;

        public Shield(int count) => counter = new Counter(count);

        public Sprite Sprite => SpritesContainer.instance.shield;

        public string Tag => throw new NotImplementedException();

        public string Description => ""; // TODO
        public string SubInfo => counter.SubInfo;

        public bool ToDelete => counter.EndedWork;

        int IStatModifier.Modify(int val) => counter.Decrease(val);
        public bool ConcatAbleWith(IConcatAble other) => other is Shield;

        public void Concat(IConcatAble other)
        {
            counter.count += ((Shield)other).counter.count;
        }
    }
}