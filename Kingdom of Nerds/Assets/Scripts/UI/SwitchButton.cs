using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    private int state = 0;
    public string switchName = "";
    private Image buttonImage;
    public Sprite onSprite;
    public Sprite offSprite;
    public PauseMenu pauseMenu;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        state = PlayerPrefs.GetInt(switchName, 0);
        if (state == 0) Off();
        else On();
    }

    public void Click()
    {
        if (state == 0) On();
        else Off();

        PlayerPrefs.SetInt(switchName, state);
        PlayerPrefs.Save();
    }

    private void On()
    {
        buttonImage.sprite = onSprite;
        state = 1;
        pauseMenu.SetGodMode(true);       
    }

    private void Off()
    {
        buttonImage.sprite = offSprite;
        state = 0;
        pauseMenu.SetGodMode(false);
    }
}
