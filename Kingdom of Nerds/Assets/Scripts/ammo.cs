using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyAmmo", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D bulletBody2D = gameObject.GetComponent<Rigidbody2D>();
        bulletBody2D.velocity = Vector2.right * speed * Time.deltaTime;

    }

    void DestroyAmmo()
    {
        Destroy(gameObject);
    }

   
}
