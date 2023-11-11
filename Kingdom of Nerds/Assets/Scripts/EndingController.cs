using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    public static void ShowEnding()
    {
        Time.timeScale = 0f;
        GameObject endPanel = GameObject.FindGameObjectWithTag("EndMenu");
        if (endPanel != null) endPanel.transform.GetChild(0).gameObject.SetActive(true);

        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui != null) ui.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
