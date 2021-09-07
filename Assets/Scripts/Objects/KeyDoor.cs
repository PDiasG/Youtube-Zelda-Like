using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door
{
    public Inventory playerInventory;

    public override void Interact()
    {
        if (playerInventory.numberOfKeys > 0)
        {
            playerInventory.numberOfKeys--;
            Open();
        }
    }
}
