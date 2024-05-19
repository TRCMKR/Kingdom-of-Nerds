using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class ReceivedPerksDisplay : MonoBehaviour
{
    public LocalizeStringEvent perksString;

    private string[] perkNames = { "AutoPickUp", "BatDebuff", "RicochetBonus", "ShieldBonus" };

    public static bool flag;

    public void ShowPerks()
    {
        flag = true;
        gameObject.SetActive(true);
        PauseMenu.isPaused = true;
        Time.timeScale = 0;

        foreach (var perk in perkNames)
        {
            BoolVariable hasPerk = new BoolVariable();
            hasPerk.Value = PlayerPrefs.GetInt(perk, 0) == 1;
            perksString.StringReference[perk] = hasPerk;
        }
        perksString.RefreshString();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) Close();
    }

    public void Close()
    {
        flag = false;
        PauseMenu.isPaused = false;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
