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
        if (Stunned()) yield break;
        
        player.ChangeHp(-Damage());
        yield return new WaitForSeconds(time);
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.KillEnemy(this));
    }
}