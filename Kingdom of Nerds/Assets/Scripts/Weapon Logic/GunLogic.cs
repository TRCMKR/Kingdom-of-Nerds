using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GunLogic : MonoBehaviour, IWeapon
{
    // public float offset; // Offset = -2
    public Transform shootDirection;
    public GameObject ammo;
    public float range;
    public float timeshot;
    public float startTime;
    public float speed = 10;
    public float bulletSpread = 7;
    public int maxAmmo = 6;
    public int currentAmmo = 6;
    public int bounces;

    private bool _gun;

    private bool _shooting;

    [SerializeField] private int damage = 2;

    public virtual int Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }


    private Camera _mainCamera;
    
    void Start()
    {
        _mainCamera = Camera.main;
        currentAmmo = maxAmmo;
        if (PlayerPrefs.GetInt("RicochetBonus") == 1) bounces += 3;
    }

    // public virtual void Use()
    // {
    //     if (timeshot > 0)
    //     {
    //         timeshot -= Time.deltaTime;
    //     }
    //     else if (currentAmmo > 0) // Input.GetMouseButton(0) && 
    //     {
    //         Vector2 difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    //         float angle = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
    //         float actualAngle = angle + Random.Range(-bulletSpread, bulletSpread + 1);
    //         Vector2 direction = Quaternion.AngleAxis(actualAngle - angle, Vector3.forward) * difference.normalized;
    //             
    //         float vectorAngle = -Vector2.SignedAngle(direction, Vector2.right);
    //
    //         var bullet = Instantiate(ammo, shootDirection.position, Quaternion.AngleAxis(vectorAngle, Vector3.forward)).GetComponent<Rigidbody2D>();
    //
    //         var bulletLogic = bullet.gameObject.GetComponent<BulletLogic>();
    //         bulletLogic.time = range / speed;
    //         // bulletLogic.speed = speed;
    //         bulletLogic.maxBounces = bounces;
    //         bullet.AddForce(direction * speed);
    //         bullet.GetComponent<BulletLogic>().direction = direction * speed;
    //         timeshot = startTime;
    //         currentAmmo -= 1;
    //         UIController.TakeAmmo();
    //     }
    // }

    public virtual void unsetActive()
    {
        StopAllCoroutines();
        _shooting = false;
    }
    
    public virtual void Use(string name)
    {
        if (_shooting) return;
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        _shooting = true;
        while (true)
        {
            if (timeshot > 0)
            {
                timeshot -= Time.deltaTime;
            }
            else if (Input.GetMouseButton(0) && currentAmmo > 0) // Input.GetMouseButton(0) && 
            {
                Vector2 difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
                float actualAngle = angle + Random.Range(-bulletSpread, bulletSpread + 1);
                Vector2 direction = Quaternion.AngleAxis(actualAngle - angle, Vector3.forward) * difference.normalized;
                
                float vectorAngle = -Vector2.SignedAngle(direction, Vector2.right);

                
                var bullet = Instantiate(ammo, shootDirection.position, Quaternion.AngleAxis(vectorAngle, Vector3.forward)).GetComponent<Rigidbody2D>();

                var bulletLogic = bullet.gameObject.GetComponent<BulletLogic>();
                bulletLogic.time = range / speed;
                bulletLogic.damage = Damage;
                // bulletLogic.speed = speed;
                bulletLogic.maxBounces = bounces;
                bullet.AddForce(direction * speed);
                bullet.GetComponent<BulletLogic>().direction = direction * speed;
                timeshot = startTime;
                currentAmmo -= 1;
                UIController.TakeAmmo();
            }
            
            if (!_gun || !Input.GetMouseButton(0)) break;
            
            yield return new WaitForEndOfFrame();
        }
        
        _shooting = false;
    }
    
    void Update()
    {
        if (!Input.GetMouseButton(0) && timeshot > 0)
        {
            timeshot -= Time.deltaTime;
        }
    }
}
