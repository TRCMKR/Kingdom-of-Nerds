using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShootingGalleryStoreManager : MonoBehaviour
{
    public GameObject storePanel;
    public GameObject shot;

    public Sprite hubOn;
    private Sprite _hubOff;

    public int shotPrice = 50;

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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = hubOn;
        if (Input.GetKeyDown(KeyCode.F))
            OpenStore();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = _hubOff;
        CloseStore();
    }

    private void OpenStore()
    {
        storePanel.SetActive(true);
        shot.GetComponent<Button>().interactable = ShotsCount < 3;
        Time.timeScale = 0f;
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BuyShot()
    {
        if (PointsCount >= shotPrice)
        {
            TakePoints(shotPrice);
            ShotsCount++;
            
            shot.GetComponent<Button>().interactable = ShotsCount < 3;
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
        SceneManager.LoadScene($"Level 1.{Random.Range(1, 2)}");
    }
}
