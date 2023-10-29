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
        randEnemy = Random.Range(0, spawnEnemy.Length);
        randPoint = Random.Range(0, spawnPoint.Length);
        
        if(enemiesNow < numberOfEnemies)
        {
            if (timeBtwSpawns < 0)
            {
                Instantiate(spawnEnemy[randEnemy], spawnPoint[randPoint].transform.position, Quaternion.identity);
                timeBtwSpawns = startTimeBtwSpawns;
                enemiesNow++;
            }
            
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
