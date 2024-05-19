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

    private int _outputDamage;
    

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
        Invoke(nameof(f), time);
        // Debug.Log("check");
        _outputDamage = damage;
    }

    void f()
    {
        StartCoroutine(ToNerf());
    }

    private IEnumerator ToNerf()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _bulletCollider.enabled = false;
        yield return new WaitForSecondsRealtime(0.2f);
        _pickUpArea.enabled = true;
        gameObject.layer = 11;
        GetComponent<SpriteRenderer>().sortingLayerName = "Nerf Bullets";
        GetComponent<SpriteRenderer>().material = outline;
        if (PlayerPrefs.GetInt("AutoPickUp") == 1) Invoke(nameof(AutoPickUp), 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided) return;
        
        var obj = collision.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            obj.GetComponent<IDamageable>().TakeDamage(_outputDamage);
            // _hasCollided = true;
            // StartCoroutine(ToNerf());
            // return;
        }
        
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        Vector2 inNormal = collision.GetContact(0).normal;
        Vector2 newVelocity = Vector2.Reflect(direction, inNormal);
        //var angle = -Vector2.SignedAngle(newVelocity, Vector2.right);
        _bounces++;
        _outputDamage++;
        if (_bounces > maxBounces)  { _hasCollided = true; }
        if (_hasCollided) { StartCoroutine(ToNerf()); return; }
        
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

    private void AutoPickUp()
    {
        GameObject.FindWithTag("Player").GetComponentInChildren<GunLogic>().currentAmmo += 1;
        PickUp();
        UIController.AddAmmo();
    }
}

