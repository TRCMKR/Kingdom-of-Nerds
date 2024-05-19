using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPerks : MonoBehaviour
{
    private IDamageable playerHP;
    private GunLogic playerGun;
    private PlayerCombat playerBat;
    private Shield playerShield;
    void Awake()
    {
        playerHP = gameObject.GetComponent<IDamageable>();

        if (SceneManager.GetActiveScene().name != "Hub")
        {
            playerGun = gameObject.transform.Find("WeaponHolder").Find("Gun").GetComponent<GunLogic>();
            playerBat = gameObject.transform.Find("WeaponHolder").Find("Bat").GetComponent<PlayerCombat>();
            playerShield = gameObject.GetComponent<Shield>();
            PerksCheck();
        }
    }

    private void PerksCheck()
    {
        if (PlayerPrefs.GetInt("AmmoBonus", 0) == 1)
        {
            playerGun.maxAmmo += 5;
            playerGun.currentAmmo = playerGun.maxAmmo;
        }
        if (PlayerPrefs.GetInt("LowerSpreadBonus", 0) == 1)
        {
            playerGun.bulletSpread -= 4;
        }
        if (PlayerPrefs.GetInt("BatRangeBonus", 0) == 1)
        {
            playerBat.attackRange += 3;
        }
        if (PlayerPrefs.GetInt("BatDamageBonus", 0) == 1)
        {
            playerBat.Damage += 3;
        }
        if (PlayerPrefs.GetInt("BatReloadBonus", 0) == 1)
        {
            playerBat.attackRate -= 1f;
        }
        if (PlayerPrefs.GetInt("AmmoRangeBonus", 0) == 1)
        {
            playerGun.range += 60;
        }
        if (PlayerPrefs.GetInt("ShootRateBonus", 0) == 1)
        {
            playerGun.startTime *= 0.75f;
        }
        if (PlayerPrefs.GetInt("HealthBonus", 0) == 1)
        {
            PlayerManager.Instance.MaxHP = 20;
        }
        if (PlayerPrefs.GetInt("MoreAmmoBonus", 0) == 1)
        {
            playerGun.maxAmmo += 5;
            playerGun.currentAmmo = playerGun.maxAmmo;
        }
        if (PlayerPrefs.GetInt("BatForceBonus", 0) == 1)
        {
            playerBat.knockbackForce *= 2;
        }
        // if (PlayerPrefs.GetInt("AmmoDamageBonus", 0) == 1)
        // {
        //     playerGun.Damage += 1;
        // }
        if (PlayerPrefs.GetInt("AmmoDamageBonus", 0) == 1)
        {
            playerGun.Damage += 1;
        }
        if (PlayerPrefs.GetInt("AmmoSpeedBonus", 0) == 1)
        {
            playerGun.speed *= 1.5f;
        }
        if (PlayerPrefs.GetInt("ShieldBonus", 0) == 1)
        {
            playerShield.MaxHP += 5;
        }
        if (PlayerPrefs.GetInt("AutoPickUp", 0) == 1)
        {
            // ...
        }
        if (PlayerPrefs.GetInt("BatDebuff", 0) == 1)
        {
            // ...
        }
    }
}
