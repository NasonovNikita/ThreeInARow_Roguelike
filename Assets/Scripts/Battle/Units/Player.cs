using System;
using Audio;

namespace Battle.Units
{
    [Serializable]
    public class Player : Unit
    {
        public static PlayerData data;

        public override void Awake()
        {
            Load();
            base.Awake();
        }

        public void OnDestroy() => Save();

        public override void WasteMove()
        {
            base.WasteMove();
            if (!HasMoves) manager.StartEnemiesTurn();
            RefillMoves();
        }

        public void StartTurn() => manager.StartEnemiesTurn();

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
            spells = data.spells;
            statuses = data.statuses;
        }

        private void Save() => data = PlayerData.NewData(hp, mana, damage, manaPerGem, data);
    }
}