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
    private bool currentInput = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        CalculateMovementInput();  // Taken from online solution but I might not need to cache the bool, check if it works and then check if I can just do it as return
        MovePlayer();
    }

    private void CalculateMovementInput()
    {
        if (inputDirection == Vector3.zero)
        {
            currentInput = false;
        }
        else if (inputDirection != Vector3.zero)
        {
            currentInput = true;
        }
    }

    private void MovePlayer()
    {
        if (currentInput == true)
        {
            Vector3 movement = inputDirection.normalized * speed * Time.deltaTime;
            transform.position += (movement * Time.deltaTime * speed);
        }
    }
    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }
}
