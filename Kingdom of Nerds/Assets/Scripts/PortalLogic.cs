using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLogic : MonoBehaviour
{
    public string playerName;
    private string sceneName;
    private double timeSpent;
    private bool playerInPortal;
    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == playerName)
        {
            playerInPortal = false;
            timeSpent = 0;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == playerName)
        {
            playerInPortal = true;
            timeSpent += Time.deltaTime;
            if (timeSpent > 3)
            {
                timeSpent = 0;
                switch (sceneName)
                {
                    case "Hub":
                        SceneManager.LoadScene("Level 1");
                        break;
                    case "Level 1":
                        SceneManager.LoadScene("Level 2");
                        break;
                    case "Level 2":
                        SceneManager.LoadScene("Level 3");
                        break;
                    case "Level 3":
                        Application.Quit();
                        break;
                }
            }
        }
    }
}
