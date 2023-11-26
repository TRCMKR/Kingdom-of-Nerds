using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLogic : MonoBehaviour
{
    private string sceneName;
    private double timeSpent;
    public double timeToTeleport;
    // private bool playerInPortal;
    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // playerInPortal = false;
            timeSpent = 0;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // playerInPortal = true;
            timeSpent += Time.deltaTime;
            if (timeSpent > timeToTeleport && (SceneManager.GetActiveScene().name == "Hub" || EnemySpawner.EnemiesNow == 0))
            {
                timeSpent = 0;
                int rand1 = Random.Range(0, 2);
                int rand2 = Random.Range(0, 2);
                int rand3 = Random.Range(0, 2);
                switch (sceneName)
                {
                    case "Hub":
                        if (rand1 == 0)
                        {
                            SceneManager.LoadScene("Level 1.1");
                            break;
                        }
                        else
                        {
                            SceneManager.LoadScene("Level 1.2");
                            break;
                        }
                    case "Level 1":
                        if (rand2 == 0)
                        {
                            SceneManager.LoadScene("Level 2.1");
                            break;
                        }
                        else
                        {
                            SceneManager.LoadScene("Level 2.2");
                            break;
                        }
                    case "Level 2":
                        if (rand3 == 0)
                        {
                            SceneManager.LoadScene("Level 3.1");
                            break;
                        }
                        else
                        {
                            SceneManager.LoadScene("Level 3.2");
                            break;
                        }
                    case "Level 3":
                        EndGame();
                        break;
                }
            }
        }
    }

    void EndGame()
    {
        EndingController.ShowEnding();
    }
}