using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    abstract int HP { get; set; }
    abstract int MaxHP { get; set; }
    public void TakeDamage(int damage, GameObject sender = null);
}
