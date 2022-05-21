using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Config params
    [SerializeField] float speed = 15.0f;

    // Cached refs
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = speed * Time.deltaTime * Vector2.up;
    }
}
