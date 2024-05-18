using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShootingGalleryStoreManager : MonoBehaviour
{
    public GameObject storePanel;
    public GameObject shots1;
    public GameObject shots2;
    public GameObject shots3;

    public Sprite hubOn;
    private Sprite _hubOff;

    public GameObject buttonHelper;

    public LocalizeStringEvent buyString;

    public int shotPrice = 50;
    public static int maxShots = 3;
    public static bool gameDeclined = false;

    public static int PointsCount 
    {
        get => PlayerPrefs.GetInt("sg_points", 0);
        set
        {
            PlayerPrefs.SetInt("sg_points", value);
            PlayerPrefs.Save();
        }
    }
    public static int ShotsCount
    {
        get => PlayerPrefs.GetInt("sg_shots", 0);
        set
        {
            PlayerPrefs.SetInt("sg_shots", value);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        _hubOff = GetComponentInChildren<SpriteRenderer>().sprite;
        UIController.SetShootGalleryPoints(PointsCount);
        gameDeclined = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameDeclined) return;
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
        if (gameDeclined) return;
        GetComponentInChildren<SpriteRenderer>().sprite = _hubOff;
        buttonHelper.SetActive(false);
        CloseStore();
    }

    private void DisableShotButtons()
    {
        shots1.GetComponent<Button>().interactable = false;
        shots2.GetComponent<Button>().interactable = false;
        shots3.GetComponent<Button>().interactable = false;
    }

    private void OpenStore()
    {
        if (!gameDeclined)
        {
            storePanel.SetActive(true);
            //CheckShotButtons();
            Time.timeScale = 0f;
            PauseMenu.isPaused = true;
        }
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
        //buyString.gameObject.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
    }

    public void BuyShots(int amount)
    {
        int pointsNeeded = 5;
        if (amount == 2) pointsNeeded = 9;
        if (amount == 3) pointsNeeded = 12;

        if (PointsCount >= pointsNeeded)
        {
            TakePoints(pointsNeeded);
            ShotsCount += amount;

            for (int i = 0; i < amount; i++)
                UIController.AddAmmo();

            DisableShotButtons();

            IntVariable cnt = new IntVariable();
            cnt.Value = amount;
            buyString.StringReference["shots"] = cnt;
            buyString.gameObject.SetActive(true);
        }
    }

    public static void AddPoints(int amount)
    {
        PointsCount += amount;
        UIController.SetShootGalleryPoints(PointsCount);
    }

    public static void TakePoints(int amount)
    {
        PointsCount -= amount;
        if (PointsCount < 0) PointsCount = 0;
        UIController.SetShootGalleryPoints(PointsCount);        
    }

    public static void TakeShot(int amount = 1)
    {
        ShotsCount -= amount;
        if (ShotsCount < 0) ShotsCount = 0;
        //UIController
    }

    public void DeclineMiniGame()
    {
        CloseStore();
        GetComponentInChildren<SpriteRenderer>().sprite = _hubOff;
        gameDeclined = true;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && storePanel.activeSelf)
        {
            CloseStore();
        }
    }
}
