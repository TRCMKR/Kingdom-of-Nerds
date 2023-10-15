using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float walkSpeed;
    public SpriteRenderer spriteRenderer;
    private Vector2 _direction;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        body.velocity = _direction * walkSpeed * Time.deltaTime;
        
        if (!spriteRenderer.flipX && _direction.x < 0)
            spriteRenderer.flipX = true;
        else if (spriteRenderer.flipX && _direction.x > 0)
            spriteRenderer.flipX = false;
    }
}
