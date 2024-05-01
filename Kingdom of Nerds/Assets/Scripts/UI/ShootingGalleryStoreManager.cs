using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingGalleryStoreManager : MonoBehaviour
{
    public GameObject storePanel;
    public GameObject shot;
    private static int pointsCount;
    public int shotPrice = 50;
    public int shotCount = 0;

    public Sprite hubOn;
    private Sprite _hubOff;

    private void Start()
    {
        _hubOff = GetComponentInChildren<SpriteRenderer>().sprite;
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
        pointsCount = PlayerPrefs.GetInt("sg_points", 0);
        shotCount = PlayerPrefs.GetInt("sg_shots", 0);
        shot.GetComponent<Button>().interactable = (shotCount >= 3) ? false : true;
        Time.timeScale = 0f;
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BuyShot()
    {
        if (pointsCount >= shotPrice)
        {
            pointsCount -= shotPrice;
            shotCount++;
            if (shotCount >= 3)
                shot.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetInt("sg_shots", shotCount);
            UIController.TakeShootGalleryPoints(shotPrice);
            PlayerPrefs.Save();
        }
    }
}
