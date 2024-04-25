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
    public RectTransform attackArea;

    private Slider reloadSlider;
    private Image reloadFillImage;

    private Color chargeColor = new Color(1f, 0.5734746f, 0f, 1f);

    private void Awake()
    {
        reloadSlider = Resources.FindObjectsOfTypeAll<Slider>().Where(x => x.gameObject.name == "Reload Bar").First();
        reloadFillImage = reloadSlider.transform.Find("Fill").GetComponent<Image>();
        chargeSlider.maxValue = playerBat.maxCharge;
        reloadSlider.maxValue = playerBat.attackRate;
    }

    void Update()
    {
        if (playerBat._charging)
        {
            StartChargeProgress();
            ShowAttackArea();
        }
        else
        {
            StopChargeProgress();
            HideAttackArea();
        }

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

    public void ShowAttackArea()
    {
        float areaSize = playerBat.attackRange * 2.25f;
        attackArea.localScale = new Vector2(areaSize, areaSize);
        attackArea.gameObject.SetActive(true);
    }

    public void HideReloadBar()
    {
        reloadSlider.gameObject.SetActive(false);
    }

    public void HideAttackArea()
    {
        attackArea.gameObject.SetActive(false);
    }

    void StartChargeProgress()
    {
        chargePanel.SetActive(true);
        float time = playerBat.preview;
        chargeSlider.value = time;
        if (time <= playerBat.minCharge)
        {
            chargeSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
        else
        {
            chargeSlider.fillRect.GetComponent<Image>().color = chargeColor;
        }
    }

    void StopChargeProgress()
    {
        chargePanel.SetActive(false);
        chargeSlider.value = 0;
    }
}
