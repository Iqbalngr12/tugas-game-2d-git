using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float moveSpeed = 5f;

    private Vector3 startPosition;
    private float inputY;

    private void Start()
    {
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPosition;
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();
        inputY = movementVector.y;
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, inputY * moveSpeed);
    }
}
