using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IWeapon
{

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public float attackRate = 4f;
    float nextAttackTime = 0f;
    
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
    [SerializeField] private float preview = 0f;
    [SerializeField] private float maxCharge = 3f;
    [SerializeField] private float minCharge = 1f;
    private KeyCode _chargeAndShootKey = KeyCode.Mouse0;
    public float knockbackForce = 80f;

    private bool _charging = false;


    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Use()
    {
        if (!_charging) return;
        StartCoroutine(Charge());
    }

    IEnumerator Charge()
    {
        _charging = true;
        while (true)
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKey(_chargeAndShootKey))
                {
                    _totalCharge += Time.deltaTime;
                    preview = _totalCharge; // Mathf.Clamp(_totalCharge, minCharge, maxCharge) / maxCharge;
                    yield return null;
                }
                else if (Input.GetKeyUp(_chargeAndShootKey))
                { ;
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    break;
                }
            }
        }
        
        _charging = false;
    }

    void Attack()
    {
        //Animation

        //detect
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage
        if (_totalCharge >= minCharge)
        {
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
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
