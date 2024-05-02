using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject controlsPanel;

    private void Awake()
    {
        int langID = PlayerPrefs.GetInt("lang", 1);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[langID];
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (settingsPanel.activeSelf) settingsPanel.SetActive(false);
            if (controlsPanel.activeSelf) controlsPanel.SetActive(false);
        }
    }

    public void PlayGame()
    {
        LevelLoader.LoadLevel("Hub");
    }

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
    }
}
