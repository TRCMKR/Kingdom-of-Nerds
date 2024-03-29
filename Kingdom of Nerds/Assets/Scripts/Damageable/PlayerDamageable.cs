using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable: DamageableCharacter
{
    private void Start()
    {
        // HP = MaxHP;
        HP = PlayerManager.Instance.HP;
        UIController.UpdateHealth();
    }
    
    public override void TakeDamage(int damage, GameObject sender = null)
    {
        if (GetComponentInChildren<Bubble>() != null)
        {
            // Debug.Log("I'm invisible!");
            return;
        }
        base.TakeDamage(damage);
        UIController.UpdateHealth();
        PlayerManager.Instance.UpdateHP();
    }

    public int Heal(int healing)
    {
        HP += healing;
        UIController.UpdateHealth();
        if (HP > MaxHP)
        {
            int diff = HP - MaxHP;
            HP = MaxHP;
            return diff;
        }

        return 0;
    }

    protected override void Die()
    {
        DeathScreen.Show();
    }
}
