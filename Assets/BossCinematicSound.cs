using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCinematicSound : MonoBehaviour
{
    [SerializeField] AudioClip shotSFX;

    public void PlayShotSound()
    {
        AudioSource.PlayClipAtPoint(shotSFX, Camera.main.transform.position);
    }

}
