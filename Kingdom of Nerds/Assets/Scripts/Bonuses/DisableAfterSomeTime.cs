using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSomeTime : MonoBehaviour
{
    public float Duration;
    private float _timer = 0;
    void OnEnable()
    {
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        while(_timer < Duration)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
