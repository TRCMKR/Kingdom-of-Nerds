using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemyDamageable : EnemyDamageable
{ 
    public bool isVulnerable;
    [SerializeField] private float _vulnerableTime;
    public override void TakeDamage(int damage, GameObject sender = null)
    {
        if (isVulnerable) base.TakeDamage(damage, sender);
        if (sender?.name == "Bat") StartCoroutine(MakeVulnerable());
    }

    private IEnumerator MakeVulnerable()
    {
        isVulnerable = true;

        yield return new WaitForSeconds(_vulnerableTime);

        isVulnerable = false;
    }
}
