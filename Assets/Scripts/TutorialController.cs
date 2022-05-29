using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Tutorial info panels")]
    [SerializeField] GameObject tutorialMovement;
    [SerializeField] GameObject tutorialShooting;
    //[SerializeField] GameObject tutorialHealth;

    [Header("Parameters")]
    [SerializeField] float showDuration = 3.0f;

    // Variables
    private bool shootingTutorialHasBeenShown = false;
    //private bool gotHurtForTheFirstTime = false;  

    // Cached refs
    private GameObject player;

    private void Start()
    {
        StartCoroutine(ShowTutorialPanel(tutorialMovement));
    }

    private void Update()
    {
       
    }

    private IEnumerator ShowTutorialPanel(GameObject panel)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !shootingTutorialHasBeenShown)
        {
            StartCoroutine(ShowTutorialPanel(tutorialShooting));
            shootingTutorialHasBeenShown = true;
        }
    }
}
