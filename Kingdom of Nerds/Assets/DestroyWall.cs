using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    //private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Blocking")
        {
            Destroy(gameObject);
        }
    }

}