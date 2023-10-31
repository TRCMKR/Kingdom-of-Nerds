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

    public float smoothTime;
    public int smoothingCoef;

    void FixedUpdate()
    {
        int focusPlayer = 1;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 focus = Vector2.ClampMagnitude(mousePos, maxFocusDist);

        if (focus.magnitude < minFocusDist)
            focus = Vector3.zero;
        
        if (focus.magnitude < (minFocusDist + maxFocusDist) / 2)
            focusPlayer = smoothingCoef;

        Vector3 desiredPos = player.position + focus;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothTime / focusPlayer);

        transform.position = smoothedPos;
    }
}

