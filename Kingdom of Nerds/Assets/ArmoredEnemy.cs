using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : MonoBehaviour
{
    public float speed;
    Transform target;
    public int maxHealth = 10;
    public int healthNow = 10;

    void Start()
    {
        healthNow = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
    }


    //отсюда начинается combat system
    private bool weaponDamage = false;
    public float TimeVulnerability;
    public int damage;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        {
            weaponDamage = true;
            TimeVulnerability = 8.0f;
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(damage);
        }

            if (TimeVulnerability > 0)
        {
            TimeVulnerability -= Time.deltaTime;
            if (TimeVulnerability < 0)
            {
                weaponDamage = false;
            }
        }
    }
        public void TakeDamage(int damage)
            {
        
                if (weaponDamage)
                {
                    healthNow -= damage;

                    if (healthNow <= 0)
                    {
                        Die();
                    }
                }
            }
        void Die()
        {
            EnemySpawner.EnemiesNow--;
            Destroy(gameObject);
        }

}