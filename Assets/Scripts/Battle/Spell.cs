using Battle.Units;
using UnityEngine;

namespace Battle
{
    public abstract class Spell : ScriptableObject
    {
        [SerializeField] protected int manaCost;

        [SerializeField] public string title;

        [SerializeField] protected float value;

        [SerializeField] protected int moves;

        protected Unit unitRelated;

        protected BattleManager manager;

        public void Init(Unit unit)
        {
            unitRelated = unit;
            manager = FindFirstObjectByType<BattleManager>();
        }

        public abstract void Cast();

        protected bool CantCast()
        {
            return manager.State != BattleState.Turn || manager.player.mana < manaCost;
        }
    }
}