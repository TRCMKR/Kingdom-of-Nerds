using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageable: DamageableCharacter
{
    [SerializeField]
    public bool _isInvincible;

    public bool godmod;
    
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
        damage = gameObject.GetComponent<Shield>().TakeDamage(damage);
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
        UIController.ShowInvincibilityBar();

        yield return new WaitForSeconds(time);
        
        _isInvincible = godmod;
        UIController.HideInvincibilityBar();
    }

    protected override void Die()
    {
        gameObject.GetComponent<PlayerPerks>().LoseLocalPerks();
        Destroy(PlayerManager.Instance.gameObject);
        DeathScreen.Show();
    }
}
