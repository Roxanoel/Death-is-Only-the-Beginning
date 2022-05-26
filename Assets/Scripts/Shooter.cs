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

    [Header("SFX and VFX")]
    [SerializeField] AudioClip[] gunshotSounds;

    // Cached refs
    private float fireRateTimer;
    private AudioSource audioSource;

    private void Start()
    {
        fireRateTimer = 0;
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (fireRateTimer < fireRateInSeconds) return;

        // Shoot bullet
        GameObject firedBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);   // Rotation will need to change but for now leave as is
        firedBullet.GetComponent<Bullet>().SetBulletDirection(gameObject.transform.up, damage);
        // Play firing sound
        if (audioSource != null)
        {
            audioSource.PlayOneShot(gunshotSounds[Random.Range(0, gunshotSounds.Length)]);
        }

        fireRateTimer = 0;
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
    }
}
