using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Bosslog : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chaseRange = 5f;

    private GameObject _player;
    private Vector2 _startingPosition;
    private Vector2 _initPosition;
    private bool _checked = false;
    
    [SerializeField] private bool isChasing = false;
    [SerializeField] private bool isReloading = true;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isSpawning = false;
    [SerializeField] private bool isSpawnReloading = false;
    [SerializeField] private float reloadTimeTumbleweed;
    [SerializeField] private float reloadTimeSpawn;
    public int speed;
    
    [SerializeField] private int bounces = 0;
    public int maxBounces;
    private bool _hasCollided = false;
    private Vector3 _direction;
    private BoxCollider2D _bossCollider;
    private float _mass;
    private float _actualSpeed;

    private EnemySpawner _spawner;
    private BossDamageable _bossDamageable;

    private EnemyDamageLogic _hitLogic;
    private int _hitDamage;

    enum FightStage
    {
        Zero = 0, One = 1, Two = 2, Three = 3
    }

    private FightStage _currentFightStage = 0;
    [SerializeField] private bool stageCompleted = false;

    private SpriteRenderer _spriteRenderer;
    public List<Sprite> sprites;
    private List<PolygonCollider2D> _colliders = new List<PolygonCollider2D>(3);
    private PolygonCollider2D _currentCollider;

    private Rigidbody2D _bossRb;
    void Start()
    {
        // Debug.Log("Start");
        _player = GameObject.FindGameObjectWithTag("Player");
        _startingPosition = transform.position;
        _initPosition = transform.position;
        _bossCollider = GetComponent<BoxCollider2D>();
        _mass = GetComponent<Rigidbody2D>().mass;
        StartCoroutine(ReloadTumbleweed());
        _actualSpeed = speed * _mass * 10;
        _spawner = GameObject.FindWithTag("Spawner").GetComponent<EnemySpawner>();
        _bossDamageable = GetComponent<BossDamageable>();

        _hitLogic = GetComponent<EnemyDamageLogic>();
        _hitDamage = _hitLogic.enemyCollisionDamage;

        _colliders = GetComponents<PolygonCollider2D>().ToList();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentCollider = _colliders[0];

        _bossRb = GetComponent<Rigidbody2D>();
    }
    
    void LateUpdate()
    {
        if (!stageCompleted) StartCoroutine(ChangeStage());

        if (isAttacking || isSpawning) return;
        if (!isReloading)
        {
            _direction = (_player.transform.position - transform.position).normalized;
            StartCoroutine(TumbleweedAttack());
            return;
        }

        if (!isChasing) StartCoroutine(Chase());
    }

    void Patrol()
    {
        Debug.Log("Patroling");

        transform.position = Vector2.MoveTowards(transform.position, _startingPosition, patrolSpeed * Time.deltaTime);
        if (Vector2.Distance(_startingPosition, _initPosition) > 8f)
            _startingPosition = _initPosition;
        if (Vector2.Distance(transform.position, _startingPosition) < 0.1f)
        {
            Vector2 newPatrolPosition = _startingPosition + new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
            _startingPosition = newPatrolPosition;
        }
        
    }

    IEnumerator ChangeStage()
    {
        while (true)
        {
            if (!isSpawnReloading &&
                (_bossDamageable.HP < 0.8 * _bossDamageable.MaxHP && _currentFightStage == 0 ||
                _bossDamageable.HP < 0.5 * _bossDamageable.MaxHP && _currentFightStage == (FightStage)1 ||
                _bossDamageable.HP < 0.3 * _bossDamageable.MaxHP && _currentFightStage == (FightStage)2))
            {
                stageCompleted = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(Spawn());
                _currentFightStage++;
                break;
            }
            
            if (!stageCompleted && _currentFightStage == FightStage.Three)
            {
                stageCompleted = true;
                reloadTimeTumbleweed /= 3;
                speed += 50;
                chaseSpeed += 3;
                break;
            }

            yield return null;
        }
    }

    IEnumerator Spawn()
    {
        Debug.Log("Spawning");
        
        _spriteRenderer.sprite = sprites[2];
        _currentCollider.enabled = false;
        _colliders[2].enabled = true;
        _currentCollider = _colliders[2];
        
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isSpawning = true;
        _spawner.enemiesWaves = true;
        _bossDamageable.isInvincible = true;
        _startingPosition = _initPosition;
        while (isAttacking) yield return null;
        _hitLogic.enemyCollisionDamage = 0;
        yield return new WaitForSeconds(0.6f);
        while (true)
        {
            if (EnemySpawner.EnemiesNow == 0) break;
            Patrol();
            
            yield return null;
        }

        StartCoroutine(ReloadSpawn());
        _bossDamageable.isInvincible = false;
        _spawner.enemiesWaves = false;
        isSpawning = false;
        stageCompleted = false;
        // _checked = false;
        _hitLogic.enemyCollisionDamage = _hitDamage;
    }
    
    IEnumerator ReloadSpawn()
    {
        isSpawnReloading = true;

        yield return new WaitForSeconds(reloadTimeSpawn);

        isSpawnReloading = false;
    }

    IEnumerator Chase()
    {
        Debug.Log("Chasing");
        
        _spriteRenderer.sprite = sprites[0];
        _currentCollider.enabled = false;
        _colliders[0].enabled = true;
        _currentCollider = _colliders[0];
        
        
        isChasing = true;
        while (true)
        {
            // var direction = _player.transform.position - transform.position;
            // _bossRb.AddForce(direction.normalized * (chaseSpeed * 90), ForceMode2D.Force);
            var movePos = Vector2.MoveTowards(transform.position, _player.transform.position, chaseSpeed * Time.deltaTime);
            _bossRb.MovePosition(movePos);
            
            if (isAttacking) break; 
            yield return null;
        }
        
        isChasing = false;
    }
    
    IEnumerator TumbleweedAttack()
    {
        Debug.Log("Attacking");
        
        _spriteRenderer.sprite = sprites[1];
        _currentCollider.enabled = false;
        _colliders[1].enabled = true;
        _currentCollider = _colliders[1];
        
        isAttacking = true;
        var rb = gameObject.GetComponent<Rigidbody2D>();
        while (true)
        {
            if (_hasCollided) break;
            rb.velocity = Vector2.zero;
            rb.AddForce(_direction * (_actualSpeed * Time.deltaTime * 90));
            yield return null;
        }

        StartCoroutine(ReloadTumbleweed());
        FinishAttack();
        isAttacking = false;
    }

    IEnumerator ReloadTumbleweed()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTimeTumbleweed);

        isReloading = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided || isReloading || collision.gameObject.CompareTag("Bullet")) return;
        if (isSpawning && !isAttacking)
        {
            _startingPosition = _initPosition;
            return;
        }

        var obj = collision.gameObject;

        var rb = GetComponent<Rigidbody2D>();
        Vector2 inNormal = collision.GetContact(0).normal;
        Vector2 newDirection = Vector2.Reflect(_direction, inNormal);
        
        bounces++;
        if (bounces >= maxBounces) _hasCollided = true;
        if (_hasCollided)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }

        rb.AddForce(newDirection * (_actualSpeed * Time.deltaTime * 90));
        
        _direction = newDirection;
    }

    void FinishAttack()
    {
        bounces = 0;
        _hasCollided = false;
    }
}