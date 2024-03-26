using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : DamageableCharacter
{
    protected override void Die()
    {
        Destroy(gameObject);
        EnemySpawner.EnemiesNow--;
    }
}
