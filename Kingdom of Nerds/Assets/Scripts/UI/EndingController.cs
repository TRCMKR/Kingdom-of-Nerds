using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI pointsText;

    public static void ShowEnding()
    {
        Time.timeScale = 0f;

        UIController.AddPoint();
        PlayerPrefs.SetInt("points", UIController.pointsAmount);
        PlayerPrefs.Save();

        GameObject endPanel = GameObject.FindGameObjectWithTag("EndMenu");
        if (endPanel != null) endPanel.transform.GetChild(0).gameObject.SetActive(true);

        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui != null) ui.SetActive(false);
    }

    public void OnEnable()
    {
        pointsText.text = UIController.pointsAmount.ToString();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hub");
    }
}
