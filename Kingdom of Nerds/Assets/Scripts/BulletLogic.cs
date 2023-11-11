using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    
    public float destroyTime;
    public int damage = 2;



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
        DestroyAmmo();
        if (collision.gameObject.tag == "Enemy") 
            collision.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
        
    }

   


}
