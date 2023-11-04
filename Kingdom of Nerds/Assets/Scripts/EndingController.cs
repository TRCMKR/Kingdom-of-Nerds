using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    public static void ShowEnding()
    {
        GameObject endPanel = GameObject.FindGameObjectWithTag("EndMenu");
        if (endPanel != null) endPanel.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
