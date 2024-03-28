using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{

    public int maxHealth;
    public int health;

    private void Start()
    {
        //health = maxHealth;
        health = PlayerManager.Instance.HP;
        maxHealth = PlayerManager.Instance.MaxHP;
    }



    public void TakeDamage(int damage)
    {
        // Debug.Log("takedamage test");
        health -= damage;
        if (health <= 0)
        {
            DeathScreen.Show();
        }

        PlayerManager.Instance.HP = health;
        //GlobalControl.Instance.maxHealth = maxHealth;
        UIController.UpdateHealth();
    }

    public int Heal(int antidamage) 
    {
        // returns overheal
        health += antidamage;
        if (health > maxHealth)
        {
            int diff = health - maxHealth;
            health = maxHealth;
            return diff;
        }
        return 0;
    }



    //void Awake()
    //{
    //    health = maxHealth;

    //}
    


}

