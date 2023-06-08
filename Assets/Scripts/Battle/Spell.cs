using UnityEngine;

namespace Battle
{
    public abstract class Spell : ScriptableObject
    {
        [SerializeField] protected int manaCost;

        [SerializeField] public string title;

        protected Unit unit;

        protected BattleManager manager;

        // ReSharper disable once ParameterHidesMember
        public void Init(Unit unit)
        {
            this.unit = unit;
            manager = FindFirstObjectByType<BattleManager>();
        }

        public abstract void Cast();

        protected bool CantCast()
        {
            return manager.State != BattleState.Turn || BattleManager.player.mana < manaCost;
        }
    }
}