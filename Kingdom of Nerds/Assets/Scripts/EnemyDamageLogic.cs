using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageLogic : MonoBehaviour
{
    public int enemyCollisionDamage = 1;
    private Rigidbody2D _rBody;

    private bool _isRunning;
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.name == "Player")
        {
            collisionGameObject.GetComponent<IDamageable>().TakeDamage(enemyCollisionDamage);
        }
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!_isRunning) StartCoroutine(HitKD(collision));
    }

    IEnumerator HitKD(Collision2D collision)
    {
        _isRunning = true;
        GameObject collisionGameObject = collision.gameObject;
        collisionGameObject.GetComponent<IDamageable>().TakeDamage(enemyCollisionDamage / 3);
            
        yield return new WaitForSeconds(1f);

        _isRunning = false;
    }
}


