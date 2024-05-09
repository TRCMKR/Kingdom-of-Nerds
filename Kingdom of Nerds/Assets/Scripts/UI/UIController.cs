using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider healthBar;
    public Slider invincibilityBar;
    public GameObject pointsDisplay;
    public GameObject sgPointsDisplay;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI sgPointsText;

    public static int pointsAmount;

    private GameObject player;
    private IDamageable playerHP;
    private GunLogic playerGun;
    private PlayerCombat playerBat;
    private Shield playerShield;

    public Transform ammoDisplay;
    public GameObject ammoSprite;

    private BatChargeController batChargeController;
    private WeaponSwitch weaponSwitch;
    public Image weaponDisplay;
    public Sprite batSprite;
    public Sprite gunSprite;

    public ShieldDisplay shieldDisplay;

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
    private static Action<int> setSGPoint;
    private static Action showInvAction;
    private static Action hideInvAction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");      
        playerHP = player.GetComponent<IDamageable>();
        
        pointsAmount = PlayerPrefs.GetInt("points", 0);
        pointsText.text = pointsAmount.ToString();       

        healthBar.maxValue = playerHP.MaxHP;
        healthBar.value = playerHP.HP;

        pointsAmount = PlayerPrefs.GetInt("points", 0);
        pointsText.text = pointsAmount.ToString();

        sgPointsDisplay.SetActive(false);

        if (SceneManager.GetActiveScene().name != "Hub")
        {
            playerGun = player.transform.Find("WeaponHolder").Find("Gun").GetComponent<GunLogic>();
            playerBat = player.transform.Find("WeaponHolder").Find("Bat").GetComponent<PlayerCombat>();
            weaponSwitch = player.transform.Find("WeaponHolder").GetComponent<WeaponSwitch>();
            batChargeController = player.transform.Find("Canvas").Find("Charging").GetComponent<BatChargeController>();
            playerShield = player.GetComponent<Shield>();

            // BonusCheck();

            for (int i = 0; i < playerGun.maxAmmo; i++)
            {
                Instantiate(ammoSprite, ammoDisplay);
            }
            pointsDisplay.SetActive(false);

            if (SceneManager.GetActiveScene().name == "ShootingGallery")
            {
                sgPointsDisplay.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene().name == "Boss Level")
        {
            boss = GameObject.Find("Tyrant").GetComponent<EnemyDamageable>();           
            bossHealthSlider.maxValue = boss.MaxHP;
            bossHealthSlider.gameObject.SetActive(true);
            bossLevel = true;
        }

        if (ShieldDisplay.isShielded) shieldDisplay.Activate();

        
        hideAction = Hide;
        showAction = Show;
        updateHealth = RefreshHealth;
        takeAmmo = RemoveBullet;
        addAmmo = AddBullet;
        addPoint = AddPoints;
        takePoint = Take_Points;
        setSGPoint = SetSGPoints;
        showInvAction = ShowInvBar;
        hideInvAction = HideInvBar;
    }

    private void Update()
    {
        DisplayWeapon();

        if (bossLevel) UpdateBossHealth();

        if (Keyboard.current.rKey.wasPressedThisFrame) {playerShield.shieldHP = playerShield.MaxHP; shieldDisplay.Activate();}
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

    // private void BonusCheck()
    // {
    //     if (PlayerPrefs.GetInt("AmmoBonus", 0) == 1)
    //     {
    //         playerGun.maxAmmo += 5;
    //         playerGun.currentAmmo = playerGun.maxAmmo;
    //     }
    //     if (PlayerPrefs.GetInt("LowerSpreadBonus", 0) == 1)
    //     {
    //         playerGun.bulletSpread -= 4;
    //     }
    //     if (PlayerPrefs.GetInt("BatRangeBonus", 0) == 1)
    //     {
    //         playerBat.attackRange += 2;
    //     }
    //     if (PlayerPrefs.GetInt("BatDamageBonus", 0) == 1)
    //     {
    //         playerBat.Damage += 3;
    //     }
    //     if (PlayerPrefs.GetInt("BatReloadBonus", 0) == 1)
    //     {
    //         playerBat.attackRate -= 1.5f;
    //     }
    //     if (PlayerPrefs.GetInt("RicochetBonus", 0) == 1)
    //     {
    //         playerGun.bounces += 3;
    //     }
    //     if (PlayerPrefs.GetInt("AmmoRangeBonus", 0) == 1)
    //     {
    //         playerGun.range *= 2f;
    //     }
    //     if (PlayerPrefs.GetInt("ShootRateBonus", 0) == 1)
    //     {
    //         playerGun.startTime *= 0.75f;
    //     }
    //     if (PlayerPrefs.GetInt("HealthBonus", 0) == 1)
    //     {
    //         playerHP.MaxHP += 5;
    //         if (SceneManager.GetActiveScene().name == "Hub")
    //             playerHP.HP = playerHP.MaxHP;
    //     }
    //     if (PlayerPrefs.GetInt("MoreAmmoBonus", 0) == 1)
    //     {
    //         playerGun.maxAmmo += 5;
    //         playerGun.currentAmmo = playerGun.maxAmmo;
    //     }
    //     if (PlayerPrefs.GetInt("BatForceBonus", 0) == 1)
    //     {
    //         playerBat.knockbackForce *= 2;
    //     }
    //     if (PlayerPrefs.GetInt("AmmoDamageBonus", 0) == 1)
    //     {
    //         playerGun.Damage += 1;
    //     }
    // }

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

    public static void SetShootGalleryPoints(int amount)
    {
        setSGPoint.Invoke(amount);
    }
    public static void ShowInvincibilityBar()
    {
        showInvAction.Invoke();
    }

    public static void HideInvincibilityBar()
    {
        hideInvAction.Invoke();
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
        // if (ShieldDisplay.isShielded)
        // {
        //     shieldDisplay.DamageShield(1);
        // }
        // else
        // {
            healthBar.value = playerHP.HP;
            shieldDisplay.shieldSlider.value = playerShield.shieldHP;
        // }   
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

    private void SetSGPoints(int amount)
    {
        sgPointsText.text = amount.ToString();
    }
    private void ShowInvBar()
    {
        if (invincibilityBar.gameObject.activeSelf) invincibilityBar.gameObject.SetActive(false);
        invincibilityBar.gameObject.SetActive(true);   
    }

    private void HideInvBar()
    {
        invincibilityBar.gameObject.SetActive(false);
    }
    #endregion
}