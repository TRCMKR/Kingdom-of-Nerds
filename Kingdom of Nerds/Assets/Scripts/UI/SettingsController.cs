using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SettingsController : MonoBehaviour
{
    public void SetEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        PlayerPrefs.SetInt("lang", 0);
        PlayerPrefs.Save();
    }

    public void SetRussian()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        PlayerPrefs.SetInt("lang", 1);
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionNum)
    {
        switch (resolutionNum)
        {
            case 0: Screen.SetResolution(3840, 2160, Screen.fullScreen); break;
            case 1: Screen.SetResolution(2560, 1440, Screen.fullScreen); break;
            case 2: Screen.SetResolution(1920, 1080, Screen.fullScreen); break;
            case 3: Screen.SetResolution(1280, 720, Screen.fullScreen); break;
        }
    }
}
