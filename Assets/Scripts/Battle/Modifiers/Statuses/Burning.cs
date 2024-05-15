using System;
using Battle.Units;
using Core.Singleton;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Burning : Status
    {
        private int dmg = 20;
        private const int AdditionalDamage = 5;
        [SerializeField] private MoveCounter moveCounter;

        public Burning(int moves = 1) => 
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves, true));

        public override Sprite Sprite => ModifierSpritesContainer.Instance.burning;
        public override string Description => 
            SimpleFormatDescription(ModDescriptionsContainer.Instance.burning.Value, dmg);
        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork;

        public override void Init(Unit unit)
        {
            moveCounter.OnMove += () => unit.hp.TakeDamage(dmg); // dmg - AdditionalDamage
            // unit.hp.onTakingDamageMods.Add(new HpDamageMoveMod(Additional, moveCounter.Moves)); can be added 
            
            base.Init(unit);
        }

        protected override bool CanConcat(Modifier other) => 
            other is Burning burning && 
            burning.moveCounter.Moves == moveCounter.Moves;

        public override void Concat(Modifier other) => 
            dmg += ((Burning)other).dmg;
    }
}