using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using Other;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class Shield : UnitModifier, IIntModifier
    {
        private readonly Counter _counter;

        public Shield(int count, bool isSaved = false) : base(isSaved)
        {
            _counter = CreateChangeableSubSystem(new Counter(count));

            BattleFlowManager.Instance.OnCycleEnd += OnCycleEnd;
            return;
            void OnCycleEnd()
            {
                _counter.Kill();
                Kill();

                BattleFlowManager.Instance.OnCycleEnd -= OnCycleEnd;
            }
        }

        int IIntModifier.Modify(int val) => _counter.Decrease(val);

        public override Sprite Sprite => ModSpritesContainer.Instance.shield;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.shield.Value, _counter.Count);

        public override string SubInfo => _counter.SubInfo;
        protected override bool HiddenEndedWork => _counter.EndedWork;

        protected override bool HiddenCanConcat(Modifier other) => other is Shield;

        protected override void HiddenConcat(Modifier other)
        {
            _counter.ConcatWith(((Shield)other)._counter);
        }
    }
}