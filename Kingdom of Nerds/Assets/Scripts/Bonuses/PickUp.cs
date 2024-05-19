using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private CircleCollider2D _pickUpArea;

    void OnEnable()
    {
        _pickUpArea = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        // Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
