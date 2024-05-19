using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLogic : MonoBehaviour
{
    private GameObject _player;
    public float speed;
    
    private ArmoredEnemyDamageable _armored;
    
    private SpriteRenderer _spriteRenderer;
    public Sprite vulnerableSprite;
    private Sprite _initSprite;
    public Animator animator;

    private Vector2 _direction;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _armored = GetComponent<ArmoredEnemyDamageable>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initSprite = _spriteRenderer.sprite;
        speed = Random.Range(speed - 3f, speed + 6.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_armored.isVulnerable)
        {
            // _spriteRenderer.sprite = _initSprite;
            _direction = transform.position - _player.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
            animator.SetFloat("Speed", 2);

            if (_spriteRenderer.flipX && _direction.x < 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (!_spriteRenderer.flipX && _direction.x > 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
        else
        {
            // _spriteRenderer.sprite = vulnerableSprite;
            animator.SetFloat("Speed", 0);
        }
    }
}
