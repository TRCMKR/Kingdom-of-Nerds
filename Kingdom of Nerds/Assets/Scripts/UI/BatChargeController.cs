using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BatChargeController : MonoBehaviour
{
    public PlayerCombat playerBat;
    public GameObject chargePanel;
    public Slider chargeSlider;
    private Slider reloadSlider;
    private Image reloadFillImage;

    private void Awake()
    {
        reloadSlider = Resources.FindObjectsOfTypeAll<Slider>().Where(x => x.gameObject.name == "Reload Bar").First();
        reloadFillImage = reloadSlider.transform.Find("Fill").GetComponent<Image>();
        chargeSlider.maxValue = playerBat.maxCharge;
        reloadSlider.maxValue = playerBat.attackRate;
    }

    void Update()
    {
        if (playerBat._charging) StartChargeProgress();
        else StopChargeProgress();

        if (playerBat.currentTime > 0)
        {
            float time = playerBat.attackRate - playerBat.currentTime;
            reloadSlider.value = time;
            reloadFillImage.color = Color.yellow;
        }
        else
        {
            reloadFillImage.color = Color.green;
        }
    }

    public void ShowReloadBar()
    {
        reloadSlider.maxValue = playerBat.attackRate;
        reloadSlider.gameObject.SetActive(true);
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
