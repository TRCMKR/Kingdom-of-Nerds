using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    // public float offset; // Offset = -2
    public Transform shootDirection;
    public GameObject ammo;
    public float timeshot;
    public float startTime;
    public float speed = 10;
    public int currentAmmo = 6;


    private Camera _mainCamera;
    
    void Start()
    {
        _mainCamera = Camera.main;
    }

    
    void Update()
    {
        Vector2 difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationAngle= Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
       

        if (timeshot < 0)
        {
            if (Input.GetMouseButtonDown(0) && currentAmmo > 0) 
            {
                var bullet = Instantiate(ammo, shootDirection.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bullet.rotation = rotationAngle;
                bullet.AddForce(difference.normalized*speed);
                timeshot = startTime;
                currentAmmo -= 1;
            }
        }
        else
        {
            timeshot -= Time.deltaTime;
        }
    }
}
