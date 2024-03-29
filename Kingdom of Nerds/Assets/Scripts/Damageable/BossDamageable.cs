using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageable : EnemyDamageable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Die()
    {
        base.Die();
        EndingController.ShowEnding();
    } 
}
