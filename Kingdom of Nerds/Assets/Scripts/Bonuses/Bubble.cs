using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float Duration;
    private float _timer;
    private PlayerDamageable _player;
    void OnEnable()
    {
        _timer = 0;
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerDamageable>();
        // StartCoroutine(_player.Invincible(Duration));
        // StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        while(_timer < Duration)
        {
            Debug.Log(_timer);
            _timer += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        _player.SetInvincible(Duration);
    }
}
