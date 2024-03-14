using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask enemyLayers;
    public int AttackDamage = 2;
    public float AttackRate = 2f;
    float nextAttackTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
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
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHP>().TakeDamage(AttackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
        


}
