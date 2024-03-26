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
                enemy = Enemy1;
                enemy.GetComponent<IDamageable>().MaxHP = _enemiesData.ListEnemy1[level].MaxHealth;
                enemy.GetComponent<EnemyCharging>().ChargeForce = _enemiesData.ListEnemy1[level].ChargeForce;
                enemy.GetComponent<EnemyCharging>().ChargePeriod = _enemiesData.ListEnemy1[level].ChargePeriod;
                enemy.GetComponent<EnemyCharging>().ChargeDuration = _enemiesData.ListEnemy1[level].ChargeDuration;
                break;
            default:  // case 2:
                enemy = Enemy2;
                enemy.GetComponent<IDamageable>().MaxHP = _enemiesData.ListEnemy2[level].MaxHealth;
                enemy.GetComponent<EnemyShooting>().period = _enemiesData.ListEnemy2[level].BulletPeriod;
                enemy.GetComponent<EnemyShooting>().force = _enemiesData.ListEnemy2[level].BulletForce;
                break;
        }
        enemy.transform.position = position;
        return enemy;
    }
}
