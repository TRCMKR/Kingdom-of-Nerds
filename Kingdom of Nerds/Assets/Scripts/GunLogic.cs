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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        if (timeshot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(ammo, shotDir.position, transform.rotation);
                timeshot = startTime;
            }
        }
        else
        {
            timeshot -= Time.deltaTime;
        }
        

    }
}
