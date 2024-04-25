using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilityDisplay : MonoBehaviour
{
    private bool isInvincible = false;
    private Slider invincibilitySlider;
    private float duration = 5f;
    private float time = 0f;

    private void Awake()
    {
        invincibilitySlider = GetComponent<Slider>();
    }

    void Update()
    {
        if (isInvincible)
        {
            time += Time.deltaTime;
            invincibilitySlider.value = duration - time;
        }
    }

    private void OnEnable()
    {
        isInvincible = true;
    }

    private void OnDisable()
    {
        isInvincible = false;
        time = 0f;
    }
}
