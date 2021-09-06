using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Parent class for interactable objects 
 */

public abstract class Interactable : MonoBehaviour
{
    public bool playerInRange;
    // Sginals to control player's context clues
    public CustomSignal contextOn;
    public CustomSignal contextOff;

    public abstract void Interact();

    void Update()
    {
        // This is using the old Unity Input System. Can be updated to the new one for better support for multiple input devices
        // Check controller-support branch for updated code
        if (Input.GetButtonDown("Interaction") && playerInRange)
        {
            Interact();
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }
}
