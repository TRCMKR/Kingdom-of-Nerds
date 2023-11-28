using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // public Rigidbody2D body;
    private GameObject _player;
    public float force;
    public float range;

    public List<Sprite> sprites;

    public float period;
    public float bulletSpread = 10;
    private float _timer;

    public GameObject bullet;
    private SpriteRenderer _spriteRenderer;
    private int _previousSprite = -1;

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
        Vector2 difference = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        float actualAngle = angle + Random.Range(-bulletSpread, bulletSpread + 1);
        Vector2 direction = Quaternion.AngleAxis(actualAngle - angle, Vector3.forward) * difference.normalized;

        int sprite = 2 - (int)(Mathf.Abs(angle) / 60);

        if (sprite != _previousSprite)
            GetComponent<SpriteRenderer>().sprite = sprites[sprite];
        
        if (!_spriteRenderer.flipX && direction.x < 0)
            _spriteRenderer.flipX = true;
        else if (_spriteRenderer.flipX && direction.x > 0)
            _spriteRenderer.flipX = false;
        
        bulletBody.AddForce(direction * force);

        _previousSprite = sprite;
        Destroy(bulletBody.gameObject, range / force);
    }
}
