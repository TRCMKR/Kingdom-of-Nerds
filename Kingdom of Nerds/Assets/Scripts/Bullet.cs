using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public int bulletDamage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.transform.name == "Player")
        {
            HP.takeDamage(bulletDamage);
        }
    }

}
