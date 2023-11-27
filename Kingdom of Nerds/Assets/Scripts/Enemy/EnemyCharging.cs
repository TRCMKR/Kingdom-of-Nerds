using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharging : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _body;
    
    public float chargeForce;
    public float chargePeriod;
    public float chargeDuration;
    private float _timer;
    [Tooltip("<calm>, <charge>")]
    public string status;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();

        status = "calm";
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (status == "charge")
        {
            if (_timer > chargeDuration)
            {
                _timer = 0;
                status = "calm";
                ChargeOff();
            }
            return;
        }

        if (_timer > chargePeriod)
        {
            _timer = 0;
            status = "charge";
            ChargeOn();
        }   
    }

    void ChargeOn()
    {
        Vector2 direction = _player.transform.position - transform.position;
        
        if (!_spriteRenderer.flipX && direction.x < 0)
            _spriteRenderer.flipX = true;
        else if (_spriteRenderer.flipX && direction.x > 0)
            _spriteRenderer.flipX = false;
        
        _body.velocity = direction.normalized * chargeForce;
    }

    void ChargeOff()
    {
        _body.velocity = Vector2.zero;
    }
}
