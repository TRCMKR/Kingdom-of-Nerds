using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public EnemyDamageable enemyDamageable;
    public Slider healthSlider;
    public GameObject debuffSprite;

    private void Start()
    {
        if (healthSlider != null)
            healthSlider.maxValue = enemyDamageable.MaxHP;
    }

    private void Update()
    {
        if (healthSlider != null)
        {
            if (enemyDamageable.HP == enemyDamageable.MaxHP) healthSlider.gameObject.SetActive(false);
            else
            {
                healthSlider.value = enemyDamageable.HP;
                healthSlider.gameObject.SetActive(true);
            }
        }

        if (debuffSprite != null)
        {
            if (enemyDamageable.flag) debuffSprite.SetActive(true);
            else debuffSprite.SetActive(false);
        }        
    }
}
