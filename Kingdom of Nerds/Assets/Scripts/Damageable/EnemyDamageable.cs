using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : DamageableCharacter
{
    public bool flag;
    protected virtual void Start()
    {
        HP = MaxHP;
    }
    
    protected override void Die()
    {
        Destroy(gameObject);
        EnemySpawner.EnemiesNow--;
    }

    public void DebuffFunc()
    {
        StartCoroutine(Debuff());
    }
    
    protected IEnumerator Debuff()
    {
        flag = true;

        yield return new WaitForSecondsRealtime(3f);
        
        flag = false;
    }

    public override void TakeDamage(int damage, GameObject sender = null)
    {
        if (flag) damage += 2;
        base.TakeDamage(damage, sender);
        if (sender?.name == "Bat") DebuffFunc();
    }
}
