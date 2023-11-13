using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnEnemy;
    [SerializeField]
    private List<Transform> spawnPoint;
    
    public float startTimeBtwSpawns;
    private float _timeBtwSpawns;
    
    public int maxEnemies;
    public static int EnemiesNow;
    private int _enemiesSpawned;
    
    private GameObject _enemy;

    public bool spawn = true;

    void Start()
    {
        EnemiesNow = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _timeBtwSpawns = startTimeBtwSpawns;
    }
    void Update()
    {
        int randEnemy = Random.Range(0, spawnEnemy.Count);
        
        if (spawn && _enemiesSpawned < maxEnemies)
        {
            if (_timeBtwSpawns < 0)
            {
                _enemy = spawnEnemy[randEnemy];
                _enemy.transform.position = spawnPoint[randEnemy].position; // пока так
                Instantiate(_enemy);
                _enemiesSpawned++;
                EnemiesNow++;
                
                _timeBtwSpawns = startTimeBtwSpawns;
            }

            _timeBtwSpawns -= Time.deltaTime;

            if (_enemiesSpawned == maxEnemies)
                spawn = false;
        }

    }
}




