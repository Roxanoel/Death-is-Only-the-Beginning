using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Config params
    [SerializeField] float speed = 15.0f;
    [SerializeField] float lifetimeInSeconds = 3.0f;

    // Cached refs
    private Rigidbody2D rb;
    private Vector3 forward;
    private bool directionWasSetup = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, lifetimeInSeconds);
    }

    void FixedUpdate()
    {
        rb.velocity = speed * Time.deltaTime * forward;
    }

    public void SetBulletDirection(Vector3 playerDirection)
    {
        forward = playerDirection;
        transform.up = playerDirection;
    }
}
