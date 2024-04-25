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
        foreach (GameObject b in bonuses)
        {
            string name = b.GetComponent<Bonus>().name;
            b.GetComponent<Button>().interactable = (PlayerPrefs.GetInt(name, 0) == 0) ? true : false;
        }
        Time.timeScale = 0f;
        PauseMenu.isPaused = true;
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
            bonus.GetComponent<Button>().interactable = false;
        }
        
        PlayerPrefs.SetInt(bonus.name, 1);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && storePanel.activeSelf)
        {
            CloseStore();
        }
    }
}
