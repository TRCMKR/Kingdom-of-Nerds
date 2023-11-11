using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int health = 10;
    public int healthNow = 10;


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
        Destroy(gameObject);
    }
}
