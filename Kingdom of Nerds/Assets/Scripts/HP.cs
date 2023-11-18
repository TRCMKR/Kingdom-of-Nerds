using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public void TakeDamage(int damage)
    {

        health -= damage;
        if (health <= 0)
        {
            DeathScreen.Show();
        }
        UIController.UpdateHealth();
    }
    void Awake()
    {
        maxHealth = health;
    }
    
}

