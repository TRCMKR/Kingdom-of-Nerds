using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float destroyTime;
    public int damage = 2;
    private bool _hasCollided;
    public Material outline;
    

    private CircleCollider2D _pickUpArea;
    private BoxCollider2D _bulletCollider;
    
  
    void Start()
    {
        _pickUpArea = GetComponent<CircleCollider2D>();
        _bulletCollider = GetComponent<BoxCollider2D>();
        _pickUpArea.enabled = false;
        Invoke(nameof(ToNerf), destroyTime);
       
    }

    private void ToNerf()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _bulletCollider.enabled = false;
        _pickUpArea.enabled = true;
        gameObject.layer = 11;
        GetComponent<SpriteRenderer>().sortingLayerName = "Nerf Bullets";
        GetComponent<SpriteRenderer>().material = outline;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided) return;

        ToNerf();
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
        _hasCollided = true;
      
      

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
            UIController.AddAmmo();
        }
    }




    

}

