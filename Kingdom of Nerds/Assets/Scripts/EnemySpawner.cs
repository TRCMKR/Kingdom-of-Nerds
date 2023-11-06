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
    public int numberOfEnemies;
    private int _enemiesNow;
    public GameObject enemy1;
    public GameObject enemy2;
    private GameObject _enemy;

    void Start()
    {
        _timeBtwSpawns = startTimeBtwSpawns;
    }
    void Update()
    {
        int randPoint = Random.Range(0, spawnEnemy.Count);

        if (_enemiesNow < numberOfEnemies)
        {
            if (_timeBtwSpawns < 0)
            {

                if (randPoint == 0)
                {
                    _enemy = enemy1;
                    _enemy.transform.position = spawnPoint[randPoint].position;
                }
                else
                {
                    _enemy = enemy2;
                    _enemy.transform.position = spawnPoint[randPoint].position;
                }
                Instantiate(_enemy);
                _timeBtwSpawns = startTimeBtwSpawns;
                _enemiesNow++;
            }

            _timeBtwSpawns -= Time.deltaTime;
        }
    }
}




