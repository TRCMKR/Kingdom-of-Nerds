using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    public float offset;
    public Transform shotDir;
    public GameObject ammo;
    public float timeshot;
    public float startTime;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationAngleX= Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        float rotationAngleY = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(rotationAngleX, rotationAngleY,0f);


        if (timeshot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var bullet = Instantiate(ammo, shotDir.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(difference * speed*Time.deltaTime);
                timeshot = startTime;
            }
        }
        else
        {
            timeshot -= Time.deltaTime;
        }
        

    }
}
