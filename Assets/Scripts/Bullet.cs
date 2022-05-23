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
    private float damage;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, lifetimeInSeconds);
    }

    void FixedUpdate()
    {
        rb.velocity = speed * Time.deltaTime * forward;
    }

    public void SetBulletDirection(Vector3 playerDirection, float damage)
    {
        forward = playerDirection;
        transform.up = playerDirection;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: verify if collision has a health component, if so do damage. (maybe check it is not colliding with itself? although so far that is not a problem)
        if (collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        
        Destroy(this.gameObject); // For now only destroys & doesn't check tags or layers.
    }
}
