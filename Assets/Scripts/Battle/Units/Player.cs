using System;
using Audio;
using Battle.Units.Enemies;
using Grid = Battle.Match3.Grid;

namespace Battle.Units
{
    [Serializable]
    public class Player : Unit
    {
        public static PlayerData data;

        public Enemy Target => manager.target;

        private Grid grid;

        public new void TurnOn()
        {
            data.Init(this);
            base.TurnOn();
        }

        private int CountMana()
        {
            return grid.destroyed.ContainsKey(GemType.Mana) ? grid.destroyed[GemType.Mana] * manaPerGem : 0;
        }

        public virtual void Act()
        {
            grid = FindFirstObjectByType<Grid>();
            mana.Refill(CountMana());
            Damage dmg = unitDamage.GetGemsDamage(grid.destroyed);

            UseElementOnDestroyed(grid.destroyed, Target);

            grid.ClearDestroyed();

            if (dmg.IsZero()) return;

            PToEDamageLog.Log(manager.target, this, dmg);
            manager.target.DoDamage(dmg);
        }

        public override void DoDamage(Damage dmg)
        {
            base.DoDamage(dmg);
            
            AudioManager.instance.Play(AudioEnum.PlayerHit);
        }

        protected override void NoHp()
        {
            DeathLog.Log(this);
            StartCoroutine(manager.Die());
        }

        public void Save()
        {
            data = PlayerData.NewData(this, data);
        }
    }
}