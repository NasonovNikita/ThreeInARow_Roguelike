using System;
using Audio;
using Battle.Units.Enemies;

namespace Battle.Units
{
    [Serializable]
    public class Player : Unit
    {
        public static PlayerData data;

        public Enemy Target => manager.target;

        public new void TurnOn()
        {
            data.Init(this);
            base.TurnOn();
        }

        public void Act()
        {
            base.Act(Target);
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