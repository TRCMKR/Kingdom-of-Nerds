using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IWeapon
{

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public float attackRate = 4f;
    public float currentTime = 0f;
    
    [SerializeField] private int damage = 2;
    
    public virtual int Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }
    
    private float _totalCharge = 0f;
    [SerializeField] public float preview = 0f;
    [SerializeField] public float maxCharge = 3f;
    [SerializeField] private float minCharge = 1f;
    private KeyCode _chargeAndShootKey = KeyCode.Mouse0;
    public float knockbackForce = 80f;

    public bool _charging = false;
    public bool _reloading = false;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Use()
    {
        if (_charging) return;
        StartCoroutine(Charge());
    }

    IEnumerator Charge()
    {
        _charging = true;
        while (true)
        {
            if (currentTime > 0) break;
            
            if (Input.GetKey(_chargeAndShootKey))
            {
                _totalCharge += Time.deltaTime;
                animator.SetFloat("Charge", _totalCharge / minCharge);
                preview = _totalCharge; // Mathf.Clamp(_totalCharge, minCharge, maxCharge) / maxCharge;
            }
            else
            {
                Attack();
                StartCoroutine(Reload());
                break;
            }
            
            yield return new WaitForEndOfFrame();
        }
        
        _charging = false;
    }

    IEnumerator Reload()
    {
        _reloading = true;
        currentTime = attackRate;
        
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }
        // yield return new WaitForSeconds(attackRate);

        _reloading = false;
    }

    void Attack()
    {
        //Animation

        //detect
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage
        if (_totalCharge >= minCharge)
        {
            animator.SetBool("Hit", true);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<IDamageable>().TakeDamage(Damage, gameObject);
                Vector2 direction = enemy.transform.position - transform.position; // Вычисляем направление от биты
                direction = direction.normalized; // Нормализуем направление от биты
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                rb.AddForce(direction * (Mathf.Clamp(_totalCharge, minCharge, maxCharge) / maxCharge * knockbackForce), ForceMode2D.Impulse); // Применяем отталкивающую силу
            }
        }
        _totalCharge = 0f;
        Invoke("f", 0.3f);
    }

    void f()
    {
        animator.SetFloat("Charge", 0f);
        animator.SetBool("Hit", false);
    }

void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
