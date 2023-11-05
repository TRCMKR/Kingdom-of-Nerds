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
    public GameObject Enemy1;
    public GameObject Enemy2;
    private GameObject Enemy;

    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }
    void Update()
    {
        int randSpawnPoint = Random.Range(0, spawnPoint.Length);
        randPoint = Random.Range(0, spawnEnemy.Length);

        if (enemiesNow < numberOfEnemies)
        {
            if (timeBtwSpawns < 0)
            {

                if (randPoint == 0)
                {
                    Enemy = Enemy1;
                }
                else
                {
                    Enemy = Enemy2;
                }               
                Enemy.transform.position = spawnPoint[randSpawnPoint].position;
                Instantiate(Enemy);
                timeBtwSpawns = startTimeBtwSpawns;
                enemiesNow++;
            }

            timeBtwSpawns -= Time.deltaTime;
        }
    }
}




