using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float moveSpeed = 5f;

    [Header("AI Settings")]
    public bool canBeAI = false;
    public Transform ballTransform;
    public float aiDeadZone = 0.2f;

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
        rb2d.linearVelocity = Vector2.zero;
        inputY = 0;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (canBeAI && GameManager.instance.IsPlayer2AI())
        {
            return;
        }

        Vector2 movementVector = context.ReadValue<Vector2>();
        inputY = movementVector.y;
    }

    private void FixedUpdate()
    {
        if (canBeAI && GameManager.instance.IsPlayer2AI())
        {
            MoveAsAI();
        }

        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, inputY * moveSpeed);
    }

    private void MoveAsAI()
    {
        if (ballTransform == null) return;
        float distance = ballTransform.position.y - transform.position.y;
        
        if (Mathf.Abs(distance) > aiDeadZone)
        {
            inputY = Mathf.Sign(distance);
        }
        else
        {
            inputY = 0;
        }
    }
}
