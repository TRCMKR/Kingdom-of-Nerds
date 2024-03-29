using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float Duration;
    private float _timer;
    void OnEnable()
    {
        _timer = 0;
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        while(_timer < Duration)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
