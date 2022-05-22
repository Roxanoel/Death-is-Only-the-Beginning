using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Config params
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gun;
    [SerializeField] float fireRateInSeconds = 0.25f;
    [SerializeField] float damage = 1.0f;

    // Cached refs
    private float fireRateTimer;

    private void Start()
    {
        fireRateTimer = 0;
    }

    public void Shoot()
    {
        if (fireRateTimer < fireRateInSeconds) return;
        GameObject firedBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);   // Rotation will need to change but for now leave as is
        firedBullet.GetComponent<Bullet>().SetBulletDirection(gameObject.transform.up);
        fireRateTimer = 0;
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
    }
}
