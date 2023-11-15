using System;
using Audio;
using Battle;
using Battle.Units.AI;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(EnemyAI))]
public class Enemy : Unit
{
    private EnemyAI _ai;
    public new void TurnOn()
    {
        base.TurnOn();
        
        _ai = GetComponent<EnemyAI>();
    }
    public override void DoDamage(Damage dmg)
    {
        base.DoDamage(dmg);
        AudioManager.instance.Play(AudioEnum.EnemyHit);
    }

    public override void Act()
    {
        if (Stunned() || manager.State == BattleState.End) return;
        _ai.Act();
    }

    protected override void NoHp()
    {
        DeathLog.Log(this);
        StartCoroutine(manager.KillEnemy(this));
    }
}