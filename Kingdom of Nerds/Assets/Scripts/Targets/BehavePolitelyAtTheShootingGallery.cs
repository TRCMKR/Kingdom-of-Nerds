using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehavePolitelyAtTheShootingGallery : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("sg_shots", 0);
    }
}
