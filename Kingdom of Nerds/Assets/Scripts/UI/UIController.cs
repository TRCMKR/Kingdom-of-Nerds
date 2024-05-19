using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
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

    public ReceivedPerksDisplay perksDisplay;
    private bool showedPerks = false;

    public LocalizeStringEvent waveString;

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
    private static Action<int, int> updateWaveInfoAction;

    void Start()
    {
        pointsAmount = PlayerPrefs.GetInt("points", 0);
        pointsText.text = pointsAmount.ToString();       

        healthBar.maxValue = PlayerManager.Instance.MaxHP;
        healthBar.value = PlayerManager.Instance.HP;

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

            waveString.gameObject.SetActive(true);

            if (SceneManager.GetActiveScene().name == "Shooting Gallery")
            {
                sgPointsDisplay.SetActive(true);
                waveString.gameObject.SetActive(false);
            }          
        }

        if (SceneManager.GetActiveScene().name == "Boss Level")
        {
            boss = GameObject.Find("Tyrant").GetComponent<EnemyDamageable>();           
            bossHealthSlider.maxValue = boss.MaxHP;
            bossHealthSlider.gameObject.SetActive(true);
            bossLevel = true;
            waveString.gameObject.SetActive(false);
        }

        if (ShieldDisplay.isShielded) shieldDisplay.Activate();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<IDamageable>();

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
        updateWaveInfoAction = SetWaveCount;
    }

    private void Update()
    {
        DisplayWeapon();

        if (bossLevel) UpdateBossHealth();

        if (Keyboard.current.rKey.wasPressedThisFrame) {playerShield.shieldHP = playerShield.MaxHP; shieldDisplay.Activate();}

        CheckShootingGalleryEnd();
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

    private void CheckShootingGalleryEnd()
    {
        if (!showedPerks && SceneManager.GetActiveScene().name == "Shooting Gallery")
        {
            if (playerGun.maxAmmo != 0 && playerGun.currentAmmo == 0)
            {
                player.GetComponent<PlayerMovement>().flag = true;
                Invoke("ShowPerks", 2);
                showedPerks = true;
            }
        }
    }

    private void ShowPerks()
    {
        perksDisplay.ShowPerks();
    }

    private void UpdateBossHealth()
    {
        bossHealthSlider.value = boss.HP;
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

    public static void UpdateWaveCount(int wavesPassed, int totalWaves)
    {
        updateWaveInfoAction.Invoke(wavesPassed, totalWaves);
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

        if (playerShield != null)
        {
            if (playerShield.shieldHP <= 0) shieldDisplay.gameObject.SetActive(false);
            shieldDisplay.shieldSlider.value = playerShield.shieldHP;
        }  
    }

    private void RemoveBullet()
    {
        int lastBulletID = ammoDisplay.childCount - 1;
        GameObject lastBulletObj = ammoDisplay.GetChild(lastBulletID).gameObject;
        Destroy(lastBulletObj);
    }

    private void AddBullet()
    {
        if (SceneManager.GetActiveScene().name == "Shooting Gallery")
        {
            playerGun.maxAmmo += 1;
            playerGun.GetComponent<GunLogic>().currentAmmo += 1;
        }
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

    private void SetWaveCount(int wavesPassed, int totalWaves)
    {
        IntVariable wp = new IntVariable();
        wp.Value = wavesPassed;
        waveString.StringReference["WaveCount"] = wp;

        IntVariable tw = new IntVariable();
        tw.Value = totalWaves;
        waveString.StringReference["TotalWaves"] = tw;

        waveString.RefreshString();
    }
    #endregion
}