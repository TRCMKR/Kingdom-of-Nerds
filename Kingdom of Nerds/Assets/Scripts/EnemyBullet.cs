using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int bulletDamage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.name == "Player")
        {
            collisionGameObject.GetComponent<HP>().TakeDamage(bulletDamage);
        }
    }

}
