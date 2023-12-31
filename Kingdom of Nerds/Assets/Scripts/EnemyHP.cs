using System;
using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 10;
    public int healthNow = 10;

    private void Start()
    {
        healthNow = maxHealth;
    }


    public void TakeDamage( int damage)
    {
        healthNow -= damage;

        if (healthNow <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EnemySpawner.EnemiesNow--;
        Destroy(gameObject);
    }
}
