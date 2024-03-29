using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    
    [SerializeField] private EnemiesData _enemiesData;

    public GameObject CreateEnemy(int type, int level, Vector3 position)
    {
        GameObject enemy;
        switch (type)
        {
            case 1:
                var factoryEnemy1 = _enemiesData.ListEnemy1[level];
                enemy = Instantiate(Enemy1);
                enemy.GetComponent<IDamageable>().MaxHP = factoryEnemy1.MaxHealth;
                enemy.GetComponent<EnemyCharging>().ChargeForce = factoryEnemy1.ChargeForce;
                enemy.GetComponent<EnemyCharging>().ChargePeriod = factoryEnemy1.ChargePeriod;
                enemy.GetComponent<EnemyCharging>().ChargeDuration = factoryEnemy1.ChargeDuration;
                break;
            default:  // case 2:
                var factoryEnemy2 = _enemiesData.ListEnemy2[level];
                enemy = Instantiate(Enemy2);
                enemy.GetComponent<SpriteRenderer>().sprite = factoryEnemy2.Sprites[1];
                for (int i = 0; i < factoryEnemy2.Sprites.Count; ++i)
                    enemy.GetComponent<EnemyShooting>().sprites[i] = factoryEnemy2.Sprites[i];
                enemy.GetComponent<IDamageable>().MaxHP = factoryEnemy2.MaxHealth;
                enemy.GetComponent<EnemyShooting>().period = factoryEnemy2.BulletPeriod;
                enemy.GetComponent<EnemyShooting>().force =factoryEnemy2.BulletForce;
                enemy.GetComponent<EnemyShooting>().range =factoryEnemy2.Range;
                break;
        }
        enemy.transform.position = position;
        return enemy;
    }
}
