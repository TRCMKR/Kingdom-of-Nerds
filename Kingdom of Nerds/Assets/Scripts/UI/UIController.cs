using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject player;
    private HP playerHP;
    private GunLogic playerGun;
    public Slider healthBar;
    

    public Transform ammoDisplay;
    public GameObject ammoSprite;

    private static Action hideAction;
    private static Action showAction;
    private static Action updateHealth;
    private static Action takeAmmo;
    private static Action addAmmo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<HP>();
        playerGun = player.transform.Find("Gun").GetComponent<GunLogic>();
        healthBar.maxValue = playerHP.health;
        
        for (int i = 0; i < 12; i++)//need maxAmmo
        {
            Instantiate(ammoSprite, ammoDisplay);
        }

        hideAction += Hide;
        showAction += Show;
        updateHealth += RefreshHealth;
        takeAmmo += RemoveBullet;
        addAmmo += AddBullet;
        
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
        if (playerGun.currentAmmo < 12)//need maxAmmo
        {
            playerGun.currentAmmo++;
            Instantiate(ammoSprite, ammoDisplay);
        }
    }


    #endregion
}