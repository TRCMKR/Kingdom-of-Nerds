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
    public Slider ammoBar;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<HP>();
        playerGun = player.transform.Find("Gun").GetComponent<GunLogic>();
        healthBar.maxValue = playerHP.health;
        ammoBar.maxValue = playerGun.currentAmmo;
    }

    void Update()
    {
        healthBar.value = playerHP.health;
        ammoBar.value = playerGun.currentAmmo;
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }
}