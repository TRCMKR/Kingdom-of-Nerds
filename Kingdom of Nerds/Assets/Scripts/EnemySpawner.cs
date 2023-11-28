using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnEnemy;
    private List<Transform> _spawnPoint = new List<Transform>();

    // [SerializeField, Range(1, 3)]
    // private int _level;  // хочу, чтобы у спавнера был уровень

    public float startTimeBtwSpawns;
    private float _timeBtwSpawns;  // период волны

    public int maxEnemies;
    public int incEnemies;
    public static int EnemiesNow;
    private int _enemiesSpawned;

    private GameObject _enemy;

    public bool spawn = true; // показывает, могут ли спавниться враги в данной волне
    public bool enemiesWaves = true;  // показывает, могут ли продолжаться волны врагов

    public int enemiesWavesPassed = 0;

    public int maxWaves = 3;

    [SerializeField]
    private EnemyFactory _enemyFactory;

    void Start()
    {
        EnemiesNow = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _timeBtwSpawns = startTimeBtwSpawns;
        // _enemyFactory = GameObject.FindGameObjectsWithTag("EnemyFactory");
        
        
        foreach (Transform child in gameObject.transform)
        {
            _spawnPoint.Add(child);
        }
    }
    void Update()
    {
        if (enemiesWaves && enemiesWavesPassed < maxWaves)
        {
            if (spawn && _enemiesSpawned < maxEnemies)
            {
                if (_timeBtwSpawns < 0)
                {
                    int randEnemy = Random.Range(0, spawnEnemy.Count);
                    int randPoint = Random.Range(0, _spawnPoint.Count);
                    
                    int randomDifficulty = Random.Range(1, 101);
                    // _enemy = spawnEnemy[randEnemy];
                    if (randomDifficulty < 71)
                    {
                        _enemy = _enemyFactory.CreateEnemy(Random.Range(1, 3), 1, spawnPoint[randEnemy].position);
                    }
                    else if (randomDifficulty <  91)
                    {
                        _enemy = _enemyFactory.CreateEnemy(Random.Range(1, 3), 2, spawnPoint[randEnemy].position);
                    }
                    else
                    {
                        _enemy = _enemyFactory.CreateEnemy(Random.Range(1, 3), 3, spawnPoint[randEnemy].position);
                    }
                    // _enemy.transform.position = spawnPoint[randEnemy].position; // пока так
                    Instantiate(_enemy);
                    _enemiesSpawned++;
                    EnemiesNow++;

                    _timeBtwSpawns = startTimeBtwSpawns;
                }
                _timeBtwSpawns -= Time.deltaTime;

                if (_enemiesSpawned == maxEnemies)
                {
                    spawn = false;
                }

            }

        }
        if (enemiesWavesPassed == maxWaves)
        {
            enemiesWaves = false;
        }
        if (_enemiesSpawned == maxEnemies && EnemiesNow == 0)
        {
            enemiesWavesPassed++;
            spawn = true;
            _enemiesSpawned = 0;
            maxEnemies += incEnemies; //с каждой волной становится на одного врага больше
        }
    }
}





