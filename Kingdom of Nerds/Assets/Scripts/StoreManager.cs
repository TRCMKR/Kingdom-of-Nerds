using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public GameObject storePanel;
    public GameObject[] bonuses;
    private static int pointsCount;
    public GameObject buttonHelper;

    public Sprite hubOn;
    private Sprite _hubOff;

    private static Color boughtColor = new Color(0, 0, 0.6f, 0.1f);
    private static Color greenColor = new Color(0, 1, 0, 0.2f);
    private static Color redColor = new Color(1, 0, 0, 0.2f);

    private void Start()
    {
        _hubOff = GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = hubOn;
        buttonHelper.SetActive(true);
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenStore();
            buttonHelper.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = _hubOff;
        CloseStore();
        buttonHelper.SetActive(false);
    }

    private void OpenStore()
    {
        storePanel.SetActive(true);
        pointsCount = PlayerPrefs.GetInt("points", 0);
        CheckBonuses();
        Time.timeScale = 0f;
        PauseMenu.isPaused = true;
    }

    public void CheckBonuses()
    {
        foreach (GameObject b in bonuses)
        {
            string name = b.GetComponent<Bonus>().name;
            bool interactable = PlayerPrefs.GetInt(name, 0) == 0;
            b.GetComponent<Button>().interactable = interactable;
            if (interactable) b.GetComponent<Image>().color = pointsCount >= b.GetComponent<Bonus>().bonusCost ? greenColor : redColor;
            else b.GetComponent<Image>().color = boughtColor;
        }
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
    }

    public static void BuyBonus(Bonus bonus)
    {
        if (pointsCount >= bonus.bonusCost)
        {
            pointsCount -= bonus.bonusCost;
            UIController.TakePoints(bonus.bonusCost);
            bonus.GetComponent<Image>().color = boughtColor;
            PlayerPrefs.SetInt(bonus.name, 1);
            PlayerPrefs.Save();

            if (bonus.name == "HealthBonus")
            {
                PlayerManager.Instance.HP = 20;
                PlayerManager.Instance.MaxHP = 20;
            }
        }       
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && storePanel.activeSelf)
        {
            CloseStore();
        }
    }

    public void PerksReset()
    {
        PlayerPrefs.SetInt("AmmoBonus", 0);
        PlayerPrefs.SetInt("LowerSpreadBonus", 0);
        PlayerPrefs.SetInt("BatRangeBonus", 0);
        PlayerPrefs.SetInt("BatDamageBonus", 0);
        PlayerPrefs.SetInt("BatReloadBonus", 0);
        PlayerPrefs.SetInt("AmmoRangeBonus", 0);
        PlayerPrefs.SetInt("ShootRateBonus", 0);
        PlayerPrefs.SetInt("HealthBonus", 0);
        PlayerManager.Instance.MaxHP = 15;
        PlayerManager.Instance.HP = 15;
        PlayerPrefs.SetInt("MoreAmmoBonus", 0);
        PlayerPrefs.SetInt("BatForceBonus", 0);
        PlayerPrefs.SetInt("AmmoSpeedBonus", 0);
        PlayerPrefs.SetInt("AmmoDamageBonus", 0);
        //PlayerPrefs.GetInt("ShieldBonus", 0);
        //PlayerPrefs.GetInt("AutoPickUp", 0);
        //PlayerPrefs.GetInt("BatDebuff", 0);
        PlayerPrefs.Save();

        CheckBonuses();
    }
}
