using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused) Pause();
            else Resume();
        }
    }

    private void Pause()
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
        settingsPanel.SetActive(true);
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
    }

    public void Exit()
    {      
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
