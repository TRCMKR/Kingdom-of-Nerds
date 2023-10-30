using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharging : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D body;
    
    public float chargeForce;
    public float chargePeriod;
    public float chargeDuration;
    private float timer;
    [Tooltip("<calm>, <charge>")]
    public string status;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        status = "calm";
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (status == "charge")
        {
            if (timer > chargeDuration)
            {
                timer = 0;
                status = "calm";
                ChargeOff();
            }
            return;
        }

        if (timer > chargePeriod)
        {
            timer = 0;
            status = "charge";
            ChargeOn();
        }   
    }

    void ChargeOn()
    {
        Vector2 direction = player.transform.position - transform.position;
        body.velocity = new Vector2(direction.x, direction.y).normalized * chargeForce;
    }

    void ChargeOff()
    {
        body.velocity = Vector2.zero;
    }
}
