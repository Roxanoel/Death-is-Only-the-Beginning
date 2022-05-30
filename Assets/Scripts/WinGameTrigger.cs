using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameTrigger : MonoBehaviour
{
    [SerializeField] SceneTransition sceneTransition;

    private void OnDestroy()
    {
        sceneTransition.LoadNextScene();
    }
}
