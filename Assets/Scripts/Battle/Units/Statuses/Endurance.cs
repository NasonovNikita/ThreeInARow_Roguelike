using Battle.Modifiers;
using Battle.Units.StatModifiers;
using Core.Singleton;
using Other;
using UnityEngine;

namespace Battle.Units.Statuses
{
    /// <summary>
    ///     Get more shield when getting shield
    /// </summary>
    public class Endurance : Status
    {
        private readonly MoveCounter _counter;
        private readonly int _bonus;
        private bool _isAddedByEndurance;

        protected override bool HiddenEndedWork => _counter.EndedWork;

        public Endurance(int moves, int bonus, bool isSaved = false) : base(isSaved)
        {
            _counter = CreateChangeableSubSystem(new MoveCounter(moves));
            _bonus = bonus;
        }

        public override void Init(Unit unit)
        {
            base.Init(unit);
            if (_isAddedByEndurance)  // Prevents loop of adding shields
            {
                _isAddedByEndurance = false;
                return;
            }
            BelongingUnit.hp.onTakingDamageMods.OnModAdded += mod =>
            {
                _isAddedByEndurance = true;
                if (mod is Shield)
                    BelongingUnit.hp.onTakingDamageMods.Add(new Shield(_bonus));
            };
        }

        protected override bool HiddenCanConcat(Modifier other) => other is Endurance;

        protected override void HiddenConcat(Modifier other) => 
            _counter.Concat(((Endurance)other)._counter);

        public override Sprite Sprite => ModSpritesContainer.Instance.endurance;

        public override string Description => ModDescriptionsContainer.Instance.endurance
            .Value.IndexErrorProtectedFormat(_bonus);
        public override string SubInfo => _counter.SubInfo;
    }
}