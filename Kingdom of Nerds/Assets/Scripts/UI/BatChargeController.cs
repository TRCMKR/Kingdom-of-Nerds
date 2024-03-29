using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatChargeController : MonoBehaviour
{
    public PlayerCombat playerBat;
    public GameObject chargePanel;
    public Slider chargeSlider;
    public Slider reloadSlider;

    private bool reloadIsFinished = false;

    private void Awake()
    {
        chargeSlider.maxValue = playerBat.maxCharge;
        reloadSlider.maxValue = playerBat.attackRate;
    }

    void Update()
    {
        if (playerBat._charging) StartChargeProgress();
        else StopChargeProgress();

        if (playerBat.currentTime > 0)
        {
            reloadIsFinished = false;
            reloadSlider.value = playerBat.attackRate - playerBat.currentTime;
        }
        else
        {
            reloadIsFinished = true;
            HideReloadBar();
        }
    }

    public void ShowReloadBar()
    {
        if (!reloadIsFinished)
        {
            reloadSlider.maxValue = playerBat.attackRate;
            reloadSlider.gameObject.SetActive(true);
        }
    }

    public void HideReloadBar()
    {
        reloadSlider.gameObject.SetActive(false);
    }

    void StartChargeProgress()
    {
        chargePanel.SetActive(true);
        chargeSlider.value = playerBat.preview;
    }

    void StopChargeProgress()
    {
        chargePanel.SetActive(false);
        chargeSlider.value = 0;
    }
}
