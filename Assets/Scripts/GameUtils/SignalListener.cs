using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Signal system using scriptable objects
 */

public class SignalListener : MonoBehaviour
{
    // Signal to listen for
    public CustomSignal signal;
    // Function that will be executed when signal is raised
    public UnityEvent signalEvent;
    // Id to reuse signal in multiple entities
    public string id;

    // Add itself to list of listeners for that signal
    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    // Remove itself to list of listeners for that signal
    // Important to do this as signal is a scriptable object, so needs to reset status
    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }

    // Executed event when signal is raised
    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    // Executed event when signal is raised
    public void OnSignalRaised(string id)
    {
        if (this.id == id) signalEvent.Invoke();
    }
}
