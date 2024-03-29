using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : DamageableCharacter
{
    protected virtual void Start()
    {
        HP = MaxHP;
    }
    
    protected override void Die()
    {
        Destroy(gameObject);
        EnemySpawner.EnemiesNow--;
    }
}
