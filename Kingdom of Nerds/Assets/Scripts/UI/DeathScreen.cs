using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private static Action showDeathScreen;
    public GameObject deathPanel;
    void Awake()
    {
        showDeathScreen += ShowDeathPanel;
    }

    public static void Show()
    {
        showDeathScreen.Invoke();
    }

    private void ShowDeathPanel()
    {
        Time.timeScale = 0f;
        deathPanel.SetActive(true);
        UIController.HideUI();
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
