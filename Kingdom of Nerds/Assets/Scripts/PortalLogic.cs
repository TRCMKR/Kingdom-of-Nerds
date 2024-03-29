using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalLogic : MonoBehaviour
{
    private string _sceneName;
    private string _nextSceneName;
    private double _timeSpent;
    public double timeToTeleport;
    // private bool playerInPortal;

    public Image progressCircle;
    void Awake()
    {
        _sceneName = SceneManager.GetActiveScene().name;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // playerInPortal = false;
            _timeSpent = 0;
            UpdateTeleportProgress();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // playerInPortal = true;
            _timeSpent += Time.deltaTime;
            if (EnemySpawner.EnemiesNow == 0 || SceneManager.GetActiveScene().name == "Hub")
                UpdateTeleportProgress();
            else
                UpdateTeleportProgress(true);

            if (_timeSpent > timeToTeleport && (SceneManager.GetActiveScene().name == "Hub" || EnemySpawner.EnemiesNow == 0))
            {
                _timeSpent = 0;
                int randomNum = Random.Range(1, 3);
                if (_sceneName.Contains("Hub"))
                {
                    _nextSceneName = "Preview Level";
                    SceneManager.LoadScene(_nextSceneName);
                    return;
                }
                
                if (_sceneName.Contains("Preview"))
                    _nextSceneName = "Level 1.";
                else if (_sceneName.Contains("Level 1"))
                    _nextSceneName = "Level 2.";
                else if (_sceneName.Contains("Level 2"))
                    _nextSceneName = "Level 3.";
                else if (_sceneName.Contains("Level 3"))
                {
                    _nextSceneName = "Boss Level";
                    SceneManager.LoadScene(_nextSceneName);
                    return;
                    // EndGame();
                    // return;
                }

                SceneManager.LoadScene(_nextSceneName + randomNum);
            }
        }
    }

    void UpdateTeleportProgress(bool reset = false)
    {
        if (reset) progressCircle.fillAmount = 0;
        else progressCircle.fillAmount = (float)(_timeSpent / timeToTeleport);
    }

    void EndGame()
    {
        EndingController.ShowEnding();
    }
}