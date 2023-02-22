using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public Player player;

    private int Damage()
    {
        return baseDamage;
    }

    public override IEnumerator<WaitForSeconds> Act(float time)
    {
        if (Stunned() || manager.State == BattleState.End) yield break;
        
        yield return new WaitForSeconds(time);
        player.ChangeHp(-Damage());
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.KillEnemy(this));
    }
}