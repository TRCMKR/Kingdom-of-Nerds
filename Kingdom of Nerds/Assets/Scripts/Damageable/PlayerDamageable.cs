using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageable: DamageableCharacter
{
    private bool _isInvincible;
    
    private void Start()
    {
        // HP = MaxHP;
        HP = PlayerManager.Instance.HP;
        UIController.UpdateHealth();
    }
    
    public override void TakeDamage(int damage, GameObject sender = null)
    {
        if (_isInvincible)
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
        PlayerManager.Instance.UpdateHP();
        UIController.UpdateHealth();
        if (HP > MaxHP)
        {
            int diff = HP - MaxHP;
            HP = MaxHP;
            return diff;
        }

        return 0;
    }

    public void SetInvincible(float time)
    {
        if (_isInvincible) StopCoroutine(Invincible(time));
        StartCoroutine(Invincible(time));
    }

    private IEnumerator Invincible(float time)
    {
        _isInvincible = true;
        
        yield return new WaitForSeconds(time);
        
        _isInvincible = false;
    }

    protected override void Die()
    {
        Destroy(PlayerManager.Instance.gameObject);
        DeathScreen.Show();
    }
}
