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
    private IDamageable playerHP;
    private GunLogic playerGun;
    private PlayerCombat playerBat;

    public Transform ammoDisplay;
    public GameObject ammoSprite;

    private BatChargeController batChargeController;
    private WeaponSwitch weaponSwitch;
    public Image weaponDisplay;
    public Sprite batSprite;
    public Sprite gunSprite;

    private EnemyDamageable boss;
    private bool bossLevel = false;
    public Slider bossHealthSlider;

    private static Action hideAction;
    private static Action showAction;
    private static Action updateHealth;
    private static Action takeAmmo;
    private static Action addAmmo;
    private static Action<int> addPoint;
    private static Action<int> takePoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");      
        playerHP = player.GetComponent<IDamageable>();
        healthBar.maxValue = playerHP.MaxHP;
        healthBar.value = playerHP.HP;

        pointsAmount = PlayerPrefs.GetInt("points", 0);
        pointsText.text = pointsAmount.ToString();

        if (SceneManager.GetActiveScene().name != "Hub")
        {
            playerGun = player.transform.Find("WeaponHolder").Find("Gun").GetComponent<GunLogic>();
            playerBat = player.transform.Find("WeaponHolder").Find("Bat").GetComponent<PlayerCombat>();
            weaponSwitch = player.transform.Find("WeaponHolder").GetComponent<WeaponSwitch>();
            batChargeController = player.transform.Find("Canvas").Find("Charging").GetComponent<BatChargeController>();

            BonusCheck();

            for (int i = 0; i < playerGun.maxAmmo; i++)
            {
                Instantiate(ammoSprite, ammoDisplay);
            }
            pointsDisplay.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "Boss Level")
        {
            boss = GameObject.Find("Tyrant").GetComponent<EnemyDamageable>();           
            bossHealthSlider.maxValue = boss.MaxHP;
            bossHealthSlider.gameObject.SetActive(true);
            bossLevel = true;
        }

        hideAction = Hide;
        showAction = Show;
        updateHealth = RefreshHealth;
        takeAmmo = RemoveBullet;
        addAmmo = AddBullet;
        addPoint = AddPoints;
        takePoint = Take_Points;
    }

    private void Update()
    {
        DisplayWeapon();

        if (bossLevel) UpdateBossHealth();
    }

    private void DisplayWeapon()
    {
        if (SceneManager.GetActiveScene().name == "Hub")
        {
            weaponDisplay.gameObject.SetActive(false);
            return;
        }

        if (weaponSwitch._currentWeapon == playerGun.gameObject)
        {
            weaponDisplay.sprite = gunSprite;
            batChargeController.HideReloadBar();
        }
        else if (weaponSwitch._currentWeapon == playerBat.gameObject)
        {
            weaponDisplay.sprite = batSprite;
            batChargeController.ShowReloadBar();
        }
    }

    private void UpdateBossHealth()
    {
        bossHealthSlider.value = boss.HP;
    }

    private void BonusCheck()
    {
        if (PlayerPrefs.GetInt("AmmoBonus", 0) == 1)
        {
            playerGun.maxAmmo += 5;
            playerGun.currentAmmo = playerGun.maxAmmo;
        }
        if (PlayerPrefs.GetInt("LowerSpreadBonus", 0) == 1)
        {
            playerGun.bulletSpread -= 3;
        }
        if (PlayerPrefs.GetInt("BatRangeBonus", 0) == 1)
        {
            playerBat.attackRange += 2;
        }
        if (PlayerPrefs.GetInt("BatDamageBonus", 0) == 1)
        {
            playerBat.Damage += 1;
        }
        if (PlayerPrefs.GetInt("BatReloadBonus", 0) == 1)
        {
            playerBat.attackRate -= 1;
        }
        if (PlayerPrefs.GetInt("RicochetBonus", 0) == 1)
        {
            playerGun.bounces += 2;
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

    public static void AddPoint(int amount)
    {
        addPoint.Invoke(amount);
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
        healthBar.value = playerHP.HP;
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

    private void AddPoints(int amount)
    {
        pointsAmount += amount;
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