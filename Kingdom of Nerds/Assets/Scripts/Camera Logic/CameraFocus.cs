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
    // [SerializeField]
    // private float minFocusDist; 
    [SerializeField]
    private float maxFocusDist;

    public float smoothTime;

    void FixedUpdate()
    {
        Vector3 cameraPos = transform.position;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 focus = Vector2.ClampMagnitude(mousePos, maxFocusDist);

        // if (focus.magnitude < minFocusDist)
        //     focus = Vector3.zero;

        // if (focus.magnitude < (minFocusDist + maxFocusDist) / 2)
        //     focusPlayer = smoothingCoef;

        Vector3 desiredPos = player.position + focus;
        
        Vector3 smoothedPos = Vector3.Lerp(cameraPos, desiredPos, smoothTime); 
        smoothedPos = Vector3.Slerp(cameraPos, smoothedPos, focus.magnitude / maxFocusDist);

        transform.position = smoothedPos;
    }
}

