using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public EnemyDamageable enemyDamageable;
    public Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = enemyDamageable.MaxHP;
    }

    private void Update()
    {
        if (enemyDamageable.HP == enemyDamageable.MaxHP) healthSlider.gameObject.SetActive(false);
        else
        {
            healthSlider.value = enemyDamageable.HP;
            healthSlider.gameObject.SetActive(true);
        }
    }
}
