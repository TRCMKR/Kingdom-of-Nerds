using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider healthBar;
    public GameObject pointsDisplay;
    public TextMeshProUGUI pointsText;

    public static int pointsAmount;

    private GameObject player;
    private HP playerHP;
    private GunLogic playerGun;
    private PlayerCombat playerBat;

    public Transform ammoDisplay;
    public GameObject ammoSprite;

    private static Action hideAction;
    private static Action showAction;
    private static Action updateHealth;
    private static Action takeAmmo;
    private static Action addAmmo;
    private static Action addPoint;
    private static Action<int> takePoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<HP>();
        healthBar.maxValue = playerHP.maxHealth;
        healthBar.value = playerHP.health;

        pointsAmount = PlayerPrefs.GetInt("points", 0);
        pointsText.text = pointsAmount.ToString();

        if (SceneManager.GetActiveScene().name != "Hub")
        {
            playerGun = player.transform.Find("WeaponHolder").Find("Gun").GetComponent<GunLogic>();
            playerBat = player.transform.Find("WeaponHolder").Find("Bat").GetComponent<PlayerCombat>();
            BonusCheck();
            for (int i = 0; i < playerGun.maxAmmo; i++)
            {
                Instantiate(ammoSprite, ammoDisplay);
            }
            pointsDisplay.SetActive(false);
        }

        hideAction = Hide;
        showAction = Show;
        updateHealth = RefreshHealth;
        takeAmmo = RemoveBullet;
        addAmmo = AddBullet;
        addPoint = AddPoints;
        takePoint = Take_Points;
    }

    private void BonusCheck()
    {
        if (PlayerPrefs.GetInt("AmmoBonus", 0) == 1)
        {
            playerGun.maxAmmo += 4;
            playerGun.currentAmmo = playerGun.maxAmmo;
        }
        if (PlayerPrefs.GetInt("LowerSpreadBonus", 0) == 1)
        {
            playerGun.bulletSpread -= 3;
        }
        if (PlayerPrefs.GetInt("BatRangeBonus", 0) == 1)
        {
            playerBat.AttackRange += 1;
        }
        if (PlayerPrefs.GetInt("BatDamageBonus", 0) == 1)
        {
            playerBat.AttackDamage += 1;
        }
        if (PlayerPrefs.GetInt("RicochetBonus", 0) == 1)
        {
            //TODO
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("points", pointsAmount);
        PlayerPrefs.Save();
    }

    #region Static Actions
    public static void HideUI()
    {
        hideAction.Invoke();
    }

    public static void ShowUI()
    {
        showAction.Invoke();
    }

    public static void UpdateHealth()
    {
        updateHealth.Invoke();
    }

    public static void TakeAmmo()
    {
        takeAmmo.Invoke();
    }

    public static void AddAmmo()
    {
        addAmmo.Invoke();
    }

    public static void AddPoint()
    {
        addPoint.Invoke();
    }

    public static void TakePoints(int amount)
    {
        takePoint.Invoke(amount);
    }
    #endregion

    #region Implementations
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void RefreshHealth()
    {
        healthBar.value = playerHP.health;
    }

    private void RemoveBullet()
    {
        int lastBulletID = ammoDisplay.childCount - 1;
        GameObject lastBulletObj = ammoDisplay.GetChild(lastBulletID).gameObject;
        Destroy(lastBulletObj);
    }

    private void AddBullet()
    {
        if (playerGun.currentAmmo <= playerGun.maxAmmo)
        {
            Instantiate(ammoSprite, ammoDisplay);
        }
    }

    private void AddPoints()
    {
        pointsAmount++;
        pointsText.text = pointsAmount.ToString();
        PlayerPrefs.SetInt("points", pointsAmount);
        PlayerPrefs.Save();
    }

    private void Take_Points(int amount)
    {
        pointsAmount -= amount;
        pointsText.text = pointsAmount.ToString();
        PlayerPrefs.SetInt("points", pointsAmount);
        PlayerPrefs.Save();
    }
    #endregion
}