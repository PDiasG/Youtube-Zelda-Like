using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public FloatValue healthPerContainer;

    public override void Collect()
    {
        playerHealth.runtimeValue += healthPerContainer.runtimeValue;
        if (playerHealth.runtimeValue > heartContainers.runtimeValue * healthPerContainer.runtimeValue)
        {
            playerHealth.runtimeValue = heartContainers.runtimeValue * healthPerContainer.runtimeValue;
        }
        collectableSignal.Raise();
    }

}
