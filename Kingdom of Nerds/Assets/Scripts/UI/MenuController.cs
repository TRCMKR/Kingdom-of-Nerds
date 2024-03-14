using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        gameObject.SetActive(false);
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
