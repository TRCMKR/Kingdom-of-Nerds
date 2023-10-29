using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnEnemy;
    [SerializeField]
    private Transform[] spawnPoint;

    private int randEnemy;
    private int randPoint;

    public float startTimeBtwSpawns;
    private float timeBtwSpawns;

    public int numberOfEnemies;
    private int enemiesNow;

    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    void Update()
    {
        if(timeBtwSpawns <= 0 && enemiesNow < numberOfEnemies)
        {
            randEnemy = Random.Range(0, spawnEnemy.Length);
            randPoint = Random.Range(0, spawnPoint.Length);

            Instantiate(spawnEnemy[randEnemy], spawnPoint[randPoint].transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
            enemiesNow++;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
