using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBonus : MonoBehaviour
{
    private GameObject _player;
    void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    [SerializeField]
    public int HealValue;

    void OnDisable()
    {
        _player.GetComponent<PlayerDamageable>().Heal(HealValue);
    }
}
