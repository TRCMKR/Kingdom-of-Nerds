using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "EnemiesData", menuName = "EnemiesData", order = 51)]
public class EnemiesData : ScriptableObject
{
    [Serializable]
    public class Enemy1Data
    {
        // public string Name;
        // public int Level;
        public int MaxHealth;
        public float ChargeForce;
        public float ChargePeriod;
        public float ChargeDuration;
        public Sprite Sprite;
    }
    [Serializable]
    public class Enemy2Data
    {
        // public string Name;
        // public int Level;
        public int MaxHealth;
        public float BulletForce;
        public float BulletPeriod;
        public float Range;
        public List<Sprite> Sprites;
    }
    [Serializable]
    public class Enemy3Data
    {
        public int MaxHealth;
        public float speed;
    }

    [SerializeField] private List<Enemy1Data> _listEnemy1;
    [SerializeField] private List<Enemy2Data> _listEnemy2;

    public List<Enemy1Data> ListEnemy1 => _listEnemy1;
    public List<Enemy2Data> ListEnemy2 => _listEnemy2;
}
