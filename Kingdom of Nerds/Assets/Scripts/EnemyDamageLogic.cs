using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageLogic : MonoBehaviour
{
    public int enemyCollisionDamage = 1;
    private Rigidbody2D _rBody;
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.name == "Player")
        {
            collisionGameObject.GetComponent<HP>().TakeDamage(enemyCollisionDamage);


        }



    }

   
}