using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    private bool isInSettings = false;
    private bool isInControls = false;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isInSettings) CloseSettings();
            else if (isInControls) CloseControls();
            else
            {
                if (!isPaused) Pause();
                else Resume();
            }
        }
    }

    public void Pause()
    {      
        Time.timeScale = 0f;        
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void OpenSettings()
    {
        //UIController.HideUI();
        isInSettings = true;
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        //UIController.ShowUI();
        isInSettings = false;
        settingsPanel.SetActive(false);
    }

    public void OpenControls()
    {
        //UIController.HideUI();
        isInControls = true;
        controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        //UIController.ShowUI();
        isInControls = false;
        pausePanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void Exit()
    {      
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
