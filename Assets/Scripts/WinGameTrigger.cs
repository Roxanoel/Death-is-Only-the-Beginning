using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameTrigger : MonoBehaviour
{
    [SerializeField] SceneTransition sceneTransition;

    // Cached refs
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.onDeath += OnBossDeath;
    }

    private void OnBossDeath()
    {
        sceneTransition.LoadNextScene();
    }
}
