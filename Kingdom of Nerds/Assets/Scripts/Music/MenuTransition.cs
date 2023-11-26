using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{
    public float fadeInTime = 4f;
    public float fadeOutTime = 2f;
    private AudioSource _audio;
    private float _timeElapsed;
    private Scene _menuScene;
    private IEnumerator _fadeIn;
    private IEnumerator _fadeOut;

    private bool _flag;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _menuScene = SceneManager.GetActiveScene();
        _audio = GetComponent<AudioSource>();
        _audio.volume = 0f;
        _fadeIn = Fade(_audio, fadeInTime, 1f);
        _fadeOut = Fade(_audio, fadeOutTime, 0f);
        StartCoroutine(_fadeIn);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_flag && SceneManager.GetActiveScene() != _menuScene)
        {
            StopCoroutine(_fadeIn);
            StartCoroutine(_fadeOut);
            _flag = true;
        }

        if (_audio.volume == 0)
            Destroy(gameObject);
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
