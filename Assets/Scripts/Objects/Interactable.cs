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
    private string signalId = "player"; // interactables will only send signals to the player

    public abstract void Interact();

    protected virtual void Update()
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
        if (ContextClueCondition(other))
        {
            contextOn.Raise(signalId);
            playerInRange = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (ContextClueCondition(other))
        {
            contextOff.Raise(signalId);
            playerInRange = false;
        }
    }

    public virtual bool ContextClueCondition(Collider2D other)
    {
        return other.CompareTag("Player") && other.isTrigger;
    }
}
