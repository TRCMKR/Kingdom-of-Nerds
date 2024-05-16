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
    public Texture2D gameCursor;

    private void Awake()
    {
        int langID = PlayerPrefs.GetInt("lang", 1);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[langID];
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);//default cursor
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
        Cursor.SetCursor(gameCursor, new Vector2(16, 16), CursorMode.Auto);
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
