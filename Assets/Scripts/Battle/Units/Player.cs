using System;
using System.Collections.Generic;
using Audio;
using Battle.Modifiers.Statuses;
using Battle.Spells;

namespace Battle.Units
{
    [Serializable]
    public class Player : Unit
    {
        public static PlayerData data;

        public override List<Unit> Enemies => new(manager.Enemies);

        public override void Awake()
        {
            Load();
            base.Awake();
        }

        public void OnDestroy() => Save();

        public override void WasteMove()
        {
            base.WasteMove();
            if (HasMoves) return;
            
            manager.StartEnemiesTurn();
            RefillMoves();
        }

        public void StartTurn()
        {
            if (HasMoves) return;
            manager.StartEnemiesTurn();
        }

        public override void TakeDamage(int dmg)
        {
            base.TakeDamage(dmg);
            
            AudioManager.instance.Play(AudioEnum.PlayerHit);
        }

        protected override void NoHp()
        {
            manager.OnPlayerDeath();
            Destroy(gameObject);
        }

        private void Load()
        {
            hp = data.hp;
            mana = data.mana;
            damage = data.damage;
            manaPerGem = data.manaPerGem;
            spells = new List<Spell>(data.spells);
            statuses = new List<Status>(data.statuses);
        }

        private void Save() => data = PlayerData.NewData(this, data);
    }
}