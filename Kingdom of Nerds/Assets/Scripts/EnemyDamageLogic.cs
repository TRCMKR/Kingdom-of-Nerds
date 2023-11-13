using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageLogic : MonoBehaviour
{
    public int enemyCollisionDamage = 1;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.name == "Player")
        {
            collisionGameObject.GetComponent<HP>().TakeDamage(enemyCollisionDamage);
        }
    }
}