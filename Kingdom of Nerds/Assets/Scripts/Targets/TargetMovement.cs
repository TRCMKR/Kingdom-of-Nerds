using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Diagnostics;
using Unity.VisualScripting;
using System;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] public float Speed;
    [SerializeField] public List<Vector2> Points;
    private int _cpi = 0;  // current point index
    private float Eps = 0.9f;

    void OnEnable()
    {
        // check the invariant
        for (int i = 1; i < Points.Count; i++)
        {
        System.Diagnostics.Debug.Assert(Points[i - 1].x == Points[i].x || Points[i - 1].y == Points[i].y);
        }
        System.Diagnostics.Debug.Assert(Points[0].x == Points[^1].x || Points[0].y == Points[^1].y);

        // shift the points
        for (int i = 0; i < Points.Count; i++)
        {
            UnityEngine.Debug.Log((Vector2)gameObject.transform.position);
            Points[i] += (Vector2)gameObject.transform.position;
        }
    }

    void Update()
    {
        if (ShootingGalleryStoreManager.gameDeclined || ReceivedPerksDisplay.flag) Destroy(gameObject);
        
        Vector2 v = (-Points[_cpi] + Points[(_cpi + 1) % Points.Count]);
        v.Normalize();
        gameObject.transform.position += (Vector3) v * Speed * Time.deltaTime;
        NextPoint();
        // UnityEngine.Debug.Log((Vector2)gameObject.transform.position);
    }

    private void NextPoint()
    {
        // UnityEngine.Debug.Log(Points[_cpi]);
        if ((Points[(_cpi + 1) % Points.Count] - (Vector2) gameObject.transform.position).magnitude < Eps)
        {
            _cpi = (_cpi + 1) % Points.Count;
        }
    }
}
