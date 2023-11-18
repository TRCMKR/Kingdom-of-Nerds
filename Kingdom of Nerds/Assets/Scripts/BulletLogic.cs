using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float destroyTime;
    public int damage = 2;
    private bool hasCollided;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ToNerf), destroyTime);
    }

    private void ToNerf()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        gameObject.layer = 11;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCollided) return;

        ToNerf();
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
        hasCollided = true;
    }
    public void PickUp()
    {
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Player"))
        {
            collidedObject.GetComponentInChildren<GunLogic>().currentAmmo += 1;
            PickUp();
        }
    }
}
