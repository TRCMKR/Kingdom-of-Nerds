using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float time;
    // public float speed;
    public int damage = 2;
    private bool _hasCollided = false;
    public Material outline;
    

    private CircleCollider2D _pickUpArea;
    private BoxCollider2D _bulletCollider;
    [HideInInspector] public Vector2 direction;
    private int _bounces;
    [HideInInspector] public int maxBounces;
    
  
    void Start()
    {
        _pickUpArea = GetComponent<CircleCollider2D>();
        _bulletCollider = GetComponent<BoxCollider2D>();
        _pickUpArea.enabled = false;
        Invoke(nameof(ToNerf), time);
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

        // ToNerf();
        // if (collision.gameObject.CompareTag("Enemy"))
        //     collision.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
        // _hasCollided = true;
        //
        // if (_hasCollided) { ToNerf(); return; }


        var obj = collision.gameObject;
        //ToNerf();
        if (obj.CompareTag("Enemy"))
        {
            obj.GetComponent<IDamageable>().TakeDamage(damage);
            _hasCollided = true;
            //Debug.Log(1);
            ToNerf();
            return;
        }



        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        Vector2 inNormal = collision.GetContact(0).normal;
        Vector2 newVelocity = Vector2.Reflect(direction, inNormal);
        //var angle = -Vector2.SignedAngle(newVelocity, Vector2.right);
        _bounces++;
        if (_bounces > maxBounces)  { _hasCollided = true; }
        if (_hasCollided) { ToNerf(); return; }
        
        rb.AddForce(newVelocity);
        
        rb.rotation = -Vector2.SignedAngle(newVelocity, Vector2.right);
        direction = newVelocity;
        
        // GetComponent<BoxCollider2D>().isTrigger = true;

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

