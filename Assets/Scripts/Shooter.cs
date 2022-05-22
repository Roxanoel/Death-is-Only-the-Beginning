using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Config params
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gun;

    public void Shoot()
    {
        GameObject firedBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);   // Rotation will need to change but for now leave as is
    }
}
