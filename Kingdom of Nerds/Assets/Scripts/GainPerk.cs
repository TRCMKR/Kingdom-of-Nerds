using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPerk : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string Name;
    void OnDisable()
    {
        PlayerPrefs.SetInt(Name, 1);
    }
}
