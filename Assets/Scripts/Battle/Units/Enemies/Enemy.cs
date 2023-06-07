using System;
using Audio;

namespace Battle.Units.Enemies
{
    [Serializable]
    public class Enemy : Unit
    {
        private Player _player;

        public new void TurnOn()
        {
            base.TurnOn();

            _player = FindFirstObjectByType<Player>();
        }
        public override void DoDamage(int value)
        {
            base.DoDamage(value);

            if (value != 0)
            {
                AudioManager.instance.Play(AudioEnum.EnemyHit);
            }
        }

        public override void Act()
        {
            if (Stunned() || manager.State == BattleState.End) return;
            int doneDamage = (int)damage.GetValue();
            EToPDamageLog.Log(this, _player, doneDamage);
            _player.DoDamage(doneDamage);
        }

        protected override void NoHp()
        {
            DeathLog.Log(this);
            StartCoroutine(manager.KillEnemy(this));
        }
    }
}