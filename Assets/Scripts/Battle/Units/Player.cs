using System;
using Audio;

namespace Battle.Units
{
    [Serializable]
    public class Player : Unit
    {
        public static PlayerData data;

        public override Unit Target => manager.target;

        public new void TurnOn()
        {
            data.Init(this);
            base.TurnOn();
        }

        public override void TakeDamage(Damage dmg)
        {
            base.TakeDamage(dmg);
            
            AudioManager.instance.Play(AudioEnum.PlayerHit);
        }

        protected override void NoHp()
        {
            StartCoroutine(manager.Die());
        }

        public void Save()
        {
            data = PlayerData.NewData(this, data);
        }
    }
}