using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public int HP;
    public int MaxHP;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            MaxHP = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageable>().MaxHP;
            if (PlayerPrefs.GetInt("HealthBonus") == 1) MaxHP = 20;
            HP = MaxHP;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateHP()
    {
        Instance.HP = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageable>().HP;
        Instance.MaxHP = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageable>().MaxHP;
    }
}