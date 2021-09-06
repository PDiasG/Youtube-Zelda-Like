using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Signal system using scriptable objects
 */

[CreateAssetMenu]
public class CustomSignal : ScriptableObject
{
    public List<SignalListener> listerners = new List<SignalListener>();

    // Iterate through list of subscribed listeners and execute actions
    public void Raise()
    {
        for (int i = listerners.Count - 1; i >= 0; i--)
        {
            listerners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        listerners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener)
    {
        listerners.Remove(listener);
    }
}
