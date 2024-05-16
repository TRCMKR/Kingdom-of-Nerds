using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public int shieldHP = 0;
    // public bool isShielded = false;
    public int MaxHP = 5;

    public void Start()
    {
        shieldHP = MaxHP;
        // isShielded = true;
        // gameObject.SetActive(true);
    }

    // public void DamageShield(int damage)
    // {
    //     shieldHP -= damage;
    //     if (shieldHP <= 0)
    //     {
    //         gameObject.SetActive(false);
    //         isShielded = false;
    //         if (shieldHP < 0) UIController.UpdateHealth();
    //         return;
    //     }
    // }

    public int TakeDamage(int damage)
    {
        shieldHP -= damage;
        if (shieldHP >= 0)
            return 0;
        int overdamage = -shieldHP;
        shieldHP = 0;
        return overdamage;
    }
}
