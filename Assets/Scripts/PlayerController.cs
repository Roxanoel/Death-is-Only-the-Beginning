using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for managing player movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Config params
    [SerializeField] float speed = 1.0f;

    // Cached references
    private Rigidbody2D rb;
    private Vector3 inputDirection;
    private Shooter shooter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
    }


    void FixedUpdate()
    {
        MovePlayer(CalculateMovementInput());
    }

    private bool CalculateMovementInput()
    {
        if (inputDirection != Vector3.zero)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    private void MovePlayer(bool currentInput)
    {
        if (currentInput == true)
        {  
            Vector3 movement = inputDirection.normalized * speed * Time.deltaTime;
            rb.velocity = movement * speed; // wait why speed twice and why cache the vector3
            transform.up = rb.velocity.normalized;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    public void OnFire()
    {
        shooter.Shoot();
    }
}
