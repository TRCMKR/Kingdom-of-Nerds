using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageLogic : MonoBehaviour
{
    public int enemyCollisionDamage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            HP.takeDamage(enemyCollisionDamage);
        }
    }
}