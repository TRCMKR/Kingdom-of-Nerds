using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform player; 
    [SerializeField]
    private float minFocusDist; 
    [SerializeField]
    private float maxFocusDist;

    void FixedUpdate()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 focus = Vector2.ClampMagnitude(mousePos, maxFocusDist);

        transform.position = player.position;

        if (focus.magnitude > minFocusDist)
            transform.position += focus;
    }
}

