using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float walkSpeed;
    public SpriteRenderer spriteRenderer;
    private Vector2 _direction;

    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;

    private void Awake()
    {
        input = new CustomInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(moveVector);

        // _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _direction = moveVector;

        body.velocity = _direction * walkSpeed * Time.deltaTime;
        
        if (!spriteRenderer.flipX && _direction.x < 0)
            spriteRenderer.flipX = true;
        else if (spriteRenderer.flipX && _direction.x > 0)
            spriteRenderer.flipX = false;
    }
}
