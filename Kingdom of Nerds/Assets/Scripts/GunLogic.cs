using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    // public float offset; // Offset = -2
    public Transform shootDirection;
    public GameObject ammo;
    public float timeshot;
    public float startTime;
    public float speed = 10;
    public float bulletSpread = 7;
    public int maxAmmo = 6;
    public int currentAmmo = 6;


    private Camera _mainCamera;
    
    void Start()
    {
        _mainCamera = Camera.main;
        currentAmmo = maxAmmo;
    }

    
    void Update()
    {
        if (timeshot < 0)
        {
            if (Input.GetMouseButton(0) && currentAmmo > 0)
            {
                Vector2 difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
                float actualAngle = angle + Random.Range(-bulletSpread, bulletSpread + 1);
                Vector2 direction = Quaternion.AngleAxis(actualAngle - angle, Vector3.forward) * difference.normalized;
                
                float vectorAngle = -Vector2.SignedAngle(direction, Vector2.right);

                var bullet = Instantiate(ammo, shootDirection.position, Quaternion.AngleAxis(vectorAngle, Vector3.forward)).GetComponent<Rigidbody2D>();

                bullet.AddForce(direction * speed);
                timeshot = startTime;
                currentAmmo -= 1;
                UIController.TakeAmmo();
            }
        }
        else
        {
            timeshot -= Time.deltaTime;
        }
    }
}
