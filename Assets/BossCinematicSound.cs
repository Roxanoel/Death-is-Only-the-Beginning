using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCinematicSound : MonoBehaviour
{
    [SerializeField] AudioClip shotSFX;
    [SerializeField] Transform target1;
    [SerializeField] Transform target2;
    [SerializeField] GameObject bloodVFX;

    int shotCounter = 0;

    public void PlayShotSound()
    {
        AudioSource.PlayClipAtPoint(shotSFX, Camera.main.transform.position);
        shotCounter ++;
        if (shotCounter == 1)
        {
            Instantiate(bloodVFX, target1);
        }
        if (shotCounter == 2)
        {
            Instantiate(bloodVFX, target2);
        }
    }

}
