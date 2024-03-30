using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BossTumbleweed : MonoBehaviour
{
    private int _bounces;
    [SerializeField]
    public int maxBounces;
    // public int damage = 4;
    private bool _hasCollided = false;
    [HideInInspector] public Vector2 Direction;
    private BoxCollider2D _bossCollider;

    void Start()
    {
        // Debug.Log("Start2");
        _bossCollider = GetComponent<BoxCollider2D>();
        _bounces = 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<Bosslog>().Status != "tumbleweed")
        {
            // Debug.Log("Sorry, tumbleweed only");
            return;
        }
        if (_hasCollided)
        {
            FinishAttack();
            return;
        }

        var obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            // obj.GetComponent<IDamageable>().TakeDamage(damage);
            _hasCollided = true;
            // FinishAttack();
            return;
        }

        var rb = GetComponent<Rigidbody2D>();
        // Vector2 direction = rb.velocity;
        // direction.Normalize();
        // Debug.Log(Direction);
        Vector2 inNormal = collision.GetContact(0).normal;
        Vector2 newVelocity = Vector2.Reflect(Direction, inNormal);
        rb.velocity = Vector2.zero;
        _bounces++;
        if (_bounces > maxBounces)  { _hasCollided = true; }
        if (_hasCollided) { FinishAttack(); return; }
        
        // rb.AddForce(newVelocity);
        rb.AddForce(newVelocity * 6); // a holy net
        
        // rb.rotation = -Vector2.SignedAngle(newVelocity, Vector2.right);
        Direction = newVelocity;
    }

    void FinishAttack()
    {
        // Debug.Log("Is that all you can do?");
        gameObject.GetComponent<Bosslog>().Status = "default";
        StartCoroutine(gameObject.GetComponent<Bosslog>().ReloadTumbleweed());
    }
}
