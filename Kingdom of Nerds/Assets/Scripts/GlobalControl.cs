using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public int maxHealth;
    public int health;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    } 



    public void TakeDamage()
    {
        GlobalControl.Instance.health = health;
        GlobalControl.Instance.maxHealth = maxHealth;
    }


}
