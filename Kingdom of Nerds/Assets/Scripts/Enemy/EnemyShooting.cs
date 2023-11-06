using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // public Rigidbody2D body;
    private GameObject _player;
    public float force;

    public float period;
    private float _timer;

    public GameObject bullet;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > period)
        {
            _timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Rigidbody2D bulletBody = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        Vector2 direction = _player.transform.position - transform.position;
        
        if (!_spriteRenderer.flipX && direction.x < 0)
            _spriteRenderer.flipX = true;
        else if (_spriteRenderer.flipX && direction.x > 0)
            _spriteRenderer.flipX = false;
        
        bulletBody.AddForce(direction.normalized * force);
    }
}
