using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Transform AttackPoint;
    public float AttackRange =1f;
    public LayerMask enemyLayers;
    public int AttackDamage = 2;
    public float AttackRate = 4f;
    float nextAttackTime = 0f;

    private float totalCharge = 0f;
    private float totalChargeNeeded = 1f;
    private KeyCode chargeAndShootKey = KeyCode.Mouse0;
    public float knockbackForce = 80f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKey(chargeAndShootKey))
            {
                totalCharge += Time.deltaTime;
            }
            if (Input.GetKeyUp(chargeAndShootKey))
            {
                Attack();
                nextAttackTime = Time.time + 1f / AttackRate;
            }
          
        }
        

    }

    void Attack()
    {
        //Animation

        //detect
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);

        //Damage
        if (totalCharge >= totalChargeNeeded)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(AttackDamage);
                Vector2 direction = enemy.transform.position - transform.position; // Вычисляем направление от биты к противнику
                direction = direction.normalized; // Нормализуем вектор направления
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse); // Применяем отталкивающую силу к противник
            }
        }
        totalCharge = 0f;
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

   

}
