using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RaiseDeathEvent : MonoBehaviour
{
    public static event Action<GameObject> DeathEvent;

    private void OnDisable()
    {
        DeathEvent?.Invoke(gameObject);
    }
}
