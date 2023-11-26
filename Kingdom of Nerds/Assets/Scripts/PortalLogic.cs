using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLogic : MonoBehaviour
{
    private string _sceneName;
    private string _nextSceneName;
    private double _timeSpent;
    public double timeToTeleport;
    // private bool playerInPortal;
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
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // playerInPortal = true;
            _timeSpent += Time.deltaTime;
            if (_timeSpent > timeToTeleport && (SceneManager.GetActiveScene().name == "Hub" || EnemySpawner.EnemiesNow == 0))
            {
                _timeSpent = 0;
                int randomNum = Random.Range(1, 3);
                if (_sceneName.Contains("Hub"))
                    _nextSceneName = "Level 1.";
                else if (_sceneName.Contains("Level 1"))
                    _nextSceneName = "Level 2.";
                else if (_sceneName.Contains("Level 2"))
                    _nextSceneName = "Level 3.";
                else
                    EndGame();

                SceneManager.LoadScene(_nextSceneName + randomNum);
            }
        }
    }

    void EndGame()
    {
        EndingController.ShowEnding();
    }
}