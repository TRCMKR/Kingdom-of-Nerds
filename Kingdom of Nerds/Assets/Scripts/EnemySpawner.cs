using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool spawn = true; // показывает, могут ли спавниться враги
    public bool enemiesWaves = true;  // показывает, могут ли продолжаться волны врагов
    public int enemiesWavesPassed = 0;
    public int maxWaves = 3;
    public int randomPos = 10;

    [SerializeField]
    private EnemyFactory enemyFactory;

    private int _prevInd = -1;

    void Start()
    {
        EnemiesNow = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (GameObject.Find("Tyrant")) EnemiesNow--;
        _timeBtwSpawns = startTimeBtwSpawns;
        
        // _enemyFactory = GameObject.FindGameObjectsWithTag("EnemyFactory");
        
        
        foreach (Transform child in gameObject.transform)
        {
            _spawnPoint.Add(child);
        }
    }
    void Update()
    {
        if (enemiesWavesPassed == maxWaves)
        {
            enemiesWaves = false;
            spawn = false;
        }
        if (enemiesWaves && spawn && _enemiesSpawned < maxEnemies)
        {
            if (_timeBtwSpawns < 0)
            {
                int randEnemy = Random.Range(0, spawnEnemy.Count * 10 - 5);
                int randPoint = Random.Range(0, _spawnPoint.Count);
                float randPosX = Random.Range(-randomPos, randomPos) * Random.value;
                float randPosY = Random.Range(-randomPos, randomPos) * Random.value;
                if (randPoint == _prevInd)
                    return;
                
                int randomDifficulty = Random.Range(1, 101);
                // _enemy = spawnEnemy[randEnemy];
                if (randEnemy / 10 == 2)
                {
                    _enemy = spawnEnemy[2];
                    _enemy = Instantiate(_enemy);
                    _enemy.transform.position = _spawnPoint[randPoint].position + new Vector3(randPosX, randPosY, 0);
                }
                else if (randomDifficulty < 51)
                {
                    _enemy = enemyFactory.CreateEnemy(randEnemy / 10, 0, _spawnPoint[randPoint].position + new Vector3(randPosX, randPosY, 0));
                }
                else if (randomDifficulty <  81)
                {
                    _enemy = enemyFactory.CreateEnemy(randEnemy / 10, 1, _spawnPoint[randPoint].position + new Vector3(randPosX, randPosY, 0));
                }
                else
                {
                    _enemy = enemyFactory.CreateEnemy(randEnemy / 10, 2, _spawnPoint[randPoint].position + new Vector3(randPosX, randPosY, 0));
                }
                // _enemy.transform.position = spawnPoint[randEnemy].position; // пока так
                // Instantiate(_enemy);
                _enemiesSpawned++;
                EnemiesNow++;

                _timeBtwSpawns = startTimeBtwSpawns;

                _prevInd = randPoint;
            }
            _timeBtwSpawns -= Time.deltaTime;

            UIController.UpdateWaveCount(enemiesWavesPassed+1, maxWaves);

            if (_enemiesSpawned == maxEnemies)
            {
                spawn = false;
            }
        }
        
        if (_enemiesSpawned == maxEnemies && EnemiesNow == 0)
        {
            if (SceneManager.GetActiveScene().name == "Endless Level") maxEnemies = Random.Range(2, 8);
            enemiesWavesPassed++;
            spawn = true;
            _enemiesSpawned = 0;
            maxEnemies += incEnemies; //с каждой волной становится на incEnemies врага больше
            if (SceneManager.GetActiveScene().name == "Endless Level") ShootingGalleryStoreManager.AddPoints(1);
        }
    }
}





