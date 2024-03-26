using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _hp;
    [SerializeField] protected int _maxHp;

    public virtual int HP
    {
        get { return _hp;  }
        set
        {
            if (value > 0) 
                _hp = Mathf.Min(value, MaxHP);
            else
            {
                _hp = 0;
                Die();
            }
        }
    }

    public virtual int MaxHP
    {
        get { return _maxHp; }
        set
        {
            _maxHp = value;
        }
    }

    protected virtual void Die() { }

    public virtual void TakeDamage(int damage, GameObject sender = null)
    {
        HP -= damage;
    }
}
