using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    public Inventory playerInventory;

    public override void Collect()
    {
        if (playerInventory.coins < 1e5 - 1)
        {
            playerInventory.coins++;
        }
        collectableSignal.Raise();
    }
}
