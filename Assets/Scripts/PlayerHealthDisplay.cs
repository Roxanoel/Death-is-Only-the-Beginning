using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    // Config params
    [SerializeField] RectTransform healthBar;
    
    // Cached refs
    private Health playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    private void Update()  // Change to event later if I have time
    {
        float xScale =
        
    }
}
