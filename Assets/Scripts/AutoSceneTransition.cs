using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneTransition : SceneTransition
{
    private void Start()
    {
        LoadNextScene();
    }
}
