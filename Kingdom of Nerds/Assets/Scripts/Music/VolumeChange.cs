using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameter;

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat(parameter, Mathf.Log10(sliderValue) * 20);
    }
}
