using System;
using Battle;
using UnityEngine;

[Serializable]
public class Enemy : Unit
{
    private Player _player;

    public new void TurnOn()
    {
        base.TurnOn();

        _player = FindFirstObjectByType<Player>();
    }
    public override void DoDamage(Damage dmg)
    {
        base.DoDamage(dmg);
        AudioManager.instance.Play(AudioEnum.EnemyHit);
    }

    public override void Act()
    {
        if (Stunned() || manager.State == BattleState.End) return;
        Damage doneDamage = new Damage(0, 0, 0, 0, (int) phDmg.GetValue());
        EToPDamageLog.Log(this, _player, doneDamage);
        _player.DoDamage(doneDamage);;
    }

    protected override void NoHp()
    {
        DeathLog.Log(this);
        StartCoroutine(manager.KillEnemy(this));
    }
}