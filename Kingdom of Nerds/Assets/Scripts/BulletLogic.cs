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
        Invoke(nameof(DestroyAmmo), destroyTime);
    }

    private void DestroyAmmo()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCollided) return;
        
        DestroyAmmo();
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
        hasCollided = true;
    }
}
