using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageable: DamageableCharacter
{
    [SerializeField]
    public bool _isInvincible;
    
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
        if (_isInvincible) StopAllCoroutines();
        StartCoroutine(Invincible(time));
    }

    private IEnumerator Invincible(float time)
    {
        _isInvincible = true;
        UIController.ShowInvincibilityBar();

        yield return new WaitForSeconds(time);
        
        _isInvincible = false;
        UIController.HideInvincibilityBar();
    }

    protected override void Die()
    {
        Destroy(PlayerManager.Instance.gameObject);
        DeathScreen.Show();
    }
}
