using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public int hp;
   
    public int hpDmg;

    void Start()
    {
       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Dummy Square")
        {
            hp -= hpDmg;
            if (hp == 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}

