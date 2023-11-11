using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    [SerializeField]
    private int _hp;
    public static int hp;
    public static void takeDamage(int Damage)
    {

        hp -= Damage;
        if (hp <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    void Awake()
    {
        hp = _hp;
    }


}

