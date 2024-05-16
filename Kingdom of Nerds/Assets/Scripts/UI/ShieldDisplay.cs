using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldDisplay : MonoBehaviour
{
    public RectTransform healthBar;
    public Slider shieldSlider;
    public static int shieldHP = 0;
    public static bool isShielded = true;
    public static int MaxHP = 5;

    public void Activate()
    {
        shieldHP = MaxHP;
        shieldSlider.maxValue = MaxHP;
        shieldSlider.value = shieldHP;
        isShielded = true;
        gameObject.SetActive(true);
    }

    public void DamageShield(int damage)
    {
        shieldHP -= damage;
        if (shieldHP <= 0)
        {
            gameObject.SetActive(false);
            isShielded = false;
            if (shieldHP < 0) UIController.UpdateHealth();
            return;
        }
        shieldSlider.value = shieldHP;
    }

    private void OnEnable()
    {
        healthBar.Translate(-50, 0, 0);
    }

    private void OnDisable()
    {
        healthBar.Translate(50, 0, 0);   
    }
}
