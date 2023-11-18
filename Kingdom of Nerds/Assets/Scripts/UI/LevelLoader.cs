using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private static Action<string> loadAction;
    public GameObject loadingScreen;
    public Slider progressBar;
    public TMP_Text progressText;
    void Awake()
    {
        loadAction = Load;
    }

    public static void LoadLevel(string sceneName)
    {
        loadAction.Invoke(sceneName);
    }

    public void Load(string sceneName)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {        
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName);
        while (!sceneLoading.isDone)
        {
            float progress = sceneLoading.progress / 0.9f;
            progressBar.value = progress;
            progressText.text = $"{Mathf.RoundToInt(progress * 100)}%";
            yield return null;
        }
    }
}
