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

    [Header("Speed Settings")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxWalkSpeed;

    [Header("Curves")]
    [SerializeField] private AnimationCurve _speedFactor;
    [SerializeField] private AnimationCurve _turnFactor;
    [SerializeField] private float _maxSpeedTurnFactor;
    [SerializeField] private AnimationCurve _slowFactor;

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
        // _direction = moveVector;

        // body.velocity = _direction * walkSpeed * Time.deltaTime;
        // moveVector *= (float)1.5;
        
        if (!spriteRenderer.flipX && _direction.x < 0)
            spriteRenderer.flipX = true;
        else if (spriteRenderer.flipX && _direction.x > 0)
            spriteRenderer.flipX = false;

        float slowFactor = _slowFactor.Evaluate(body.velocity.magnitude / _maxWalkSpeed);

        if (slowFactor < body.velocity.magnitude)
            body.velocity = body.velocity.normalized * (body.velocity.magnitude - slowFactor);
        else
            body.velocity = Vector2.zero;

        if (moveVector != Vector2.zero)
        {
            float speedFactor = _speedFactor.Evaluate(body.velocity.magnitude / _maxWalkSpeed);
            float velocityDot = Vector2.Dot(body.velocity.normalized, moveVector);
            float turnFactor = _turnFactor.Evaluate(velocityDot);

            if (body.velocity.magnitude <= _maxWalkSpeed)
            {
                // Debug.Log(((turnFactor + speedFactor) * _acceleration * moveVector).magnitude);
                body.AddRelativeForce((turnFactor + speedFactor) * _acceleration * moveVector, ForceMode2D.Force);
            }
            else
            {
                body.velocity = body.velocity + Vector2.Lerp(body.velocity, body.velocity.magnitude * moveVector, 1);
            }
        }

        if (body.velocity.magnitude < _minSpeed)
        {
            body.velocity = Vector2.zero;
        }
        else if (body.velocity.magnitude > _maxWalkSpeed)
        {
            body.velocity = body.velocity.normalized * _maxWalkSpeed;
        }
    }
}
