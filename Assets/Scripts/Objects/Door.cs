using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    public DoorType doorType;
    public bool isOpen;
    public Inventory playerInventory;
    private SpriteRenderer doorSprite;
    private BoxCollider2D physicsCollider;

    private void Start()
    {
        doorSprite = GetComponentInParent<SpriteRenderer>();
        physicsCollider = GetComponentInParent<BoxCollider2D>();
    }

    public override void Interact() 
    { 
        if (doorType == DoorType.key && playerInventory.numberOfKeys > 0)
        {
            playerInventory.numberOfKeys--;
            Open();
        }
    }

    public void Open()
    {
        doorSprite.gameObject.SetActive(false);
        physicsCollider.gameObject.SetActive(false);
    }

    public void Close()
    {

    }
}
