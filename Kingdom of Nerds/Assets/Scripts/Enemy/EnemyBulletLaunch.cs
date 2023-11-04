using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLaunch : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D body;
    public float force;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = player.transform.position - transform.position;
        body.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }
}
