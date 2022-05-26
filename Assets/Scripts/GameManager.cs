using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject hudCanvas;

    public void TriggerGameOverState()
    {
        // Could add animations later but this is the basic structure
        gameOverCanvas.SetActive(true);
        hudCanvas.SetActive(false);
        Time.timeScale = 0;
    }
}
