using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void LoadNextScene()
    {
        Time.timeScale = 1;

        int indexOfSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        if(indexOfSceneToLoad < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(indexOfSceneToLoad);
        }
        else 
        {
            Debug.LogError($"Could not load next scene. Index to load: {indexOfSceneToLoad}. Number of scenes in build: {SceneManager.sceneCountInBuildSettings}");
        }
    }

    public void LoadTitleScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title screen");
    }
}
