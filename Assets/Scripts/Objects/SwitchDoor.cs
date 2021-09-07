using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This door is not interactable, it updates automatically when switch is pressed
public class SwitchDoor : Door
{
    public List<Switch> doorSwitches;
    public List<string> password;
    public int currIndex = 0;
    public CustomSignal screenKick;

    public override void Interact() { }

    public bool AddSwitch(string switchName)
    {
        if (switchName == password[currIndex])
        {
            currIndex++;
            if (currIndex == doorSwitches.Count)
            {
                Open();
            }
            return true;
        }
        else
        {
            screenKick.Raise();
            foreach(Switch _switch in doorSwitches)
            {
                _switch.DeactivateSwitch();
                currIndex = 0;
            }
            return false;
        }
    }
}
