using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;
    public Slider musicSlider;
    public Slider volumeSlider;

    private static float musicVolume = 1f;
    private static float soundVolume = 1f;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume", 1f);
        Application.quitting += SaveVolume;
        SceneManager.sceneLoaded += SaveVolume;
    }

    public void SetMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        musicVolume = sliderValue;
    }

    public void SetSoundsVolume(float sliderValue)
    {
        soundMixer.SetFloat("SoundsVol", Mathf.Log10(sliderValue) * 20);
        soundVolume = sliderValue;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
        PlayerPrefs.Save();
    }

    private void SaveVolume(Scene arg0, LoadSceneMode arg1)
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
        PlayerPrefs.Save();
    }
}
