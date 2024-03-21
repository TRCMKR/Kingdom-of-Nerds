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
        health = GlobalControl.Instance.health;
        maxHealth = GlobalControl.Instance.maxHealth;
    }



    public void TakeDamage(int damage)
    {

        health -= damage;
        if (health <= 0)
        {
            DeathScreen.Show();
        }

        GlobalControl.Instance.health = health;
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

