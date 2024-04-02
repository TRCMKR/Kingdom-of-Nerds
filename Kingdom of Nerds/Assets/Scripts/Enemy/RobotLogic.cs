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
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _armored = GetComponent<ArmoredEnemyDamageable>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initSprite = _spriteRenderer.sprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_armored.isVulnerable)
        {
            _spriteRenderer.sprite = _initSprite;
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            _spriteRenderer.sprite = vulnerableSprite;
        }
    }
}
