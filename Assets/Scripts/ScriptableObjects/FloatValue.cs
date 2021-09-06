using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using this script to kepe track of some values such as health
// Keeps consistency across scenes without need for a aingleton object
// Using ISerializationCallbackReceiver to reset values at every run
// Should only use runtimeValue on other scripts

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    [HideInInspector] public float runtimeValue;

    public void OnAfterDeserialize()
    {
        runtimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
