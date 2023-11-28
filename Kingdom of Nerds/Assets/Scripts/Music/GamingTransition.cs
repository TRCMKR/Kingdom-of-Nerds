using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamingTransition : MonoBehaviour
{
    public float fadeInTime = 4f;
    public float fadeOutTime = 2f;
    private AudioSource _audio;
    public AudioClip _loopedClip;
    private float _timeElapsed;
    private IEnumerator _fadeIn;
    private IEnumerator _fadeOut;

    private bool _flag;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _audio = GetComponent<AudioSource>();
        _audio.volume = 0f;
        _fadeIn = Fade(_audio, fadeInTime, 1f);
        _fadeOut = Fade(_audio, fadeOutTime, 0f);
        StartCoroutine(_fadeIn);
        Invoke(nameof(ChangeTrack), _audio.clip.length);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_flag && SceneManager.GetActiveScene().name == "MainMenu")
        {
            StopCoroutine(_fadeIn);
            StartCoroutine(_fadeOut);
            _flag = true;
        }

        if (_audio.volume == 0)
            Destroy(gameObject);
    }

    public void ChangeTrack()
    {
        _audio.clip = _loopedClip;
        _audio.loop = true;
        _audio.Play();
    }

    public IEnumerator Fade(AudioSource source, float duration, float targetVolume)
    {
        float time = 0f;
        float startVolume = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }
        
        yield break;
    }
}
