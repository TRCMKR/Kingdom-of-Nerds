using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageable : EnemyDamageable
{
    public bool isInvincible = false;

    public override void TakeDamage(int damage, GameObject sender = null)
    {
        if (!isInvincible) base.TakeDamage(damage);
    }
    
    protected override void Die()
    {
        base.Die();
        EndingController.ShowEnding();
    } 
}
