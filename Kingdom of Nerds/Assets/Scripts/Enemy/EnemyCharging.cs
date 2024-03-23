using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharging : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _body;
    
    public float ChargeForce;
    public float ChargePeriod;
    public float ChargeDuration;
    private float _timer;
    [Tooltip("<calm>, <charge>")]
    public string status;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _timer = Random.value;
        status = "calm";
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (status == "charge")
        {
            if (_timer > ChargeDuration)
            {
                _timer = Random.value;
                status = "calm";
                ChargeOff();
            }
            return;
        }

        if (_timer > ChargePeriod)
        {
            _timer = 0;
            status = "charge";
            ChargeOn();
        }   
    }

    public void ChargeOn()
    {
        Vector2 direction = _player.transform.position - transform.position;
        
        if (!_spriteRenderer.flipX && direction.x < 0)
            _spriteRenderer.flipX = true;
        else if (_spriteRenderer.flipX && direction.x > 0)
            _spriteRenderer.flipX = false;
        
        _body.velocity = direction.normalized * ChargeForce;
    }

    void ChargeOff()
    {
        _body.velocity = Vector2.zero;
    }


}
