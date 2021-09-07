using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public abstract class Door : Interactable
{
    public bool isOpen;
    private SpriteRenderer doorSprite;
    private BoxCollider2D physicsCollider;

    private void Start()
    {
        doorSprite = GetComponentInParent<SpriteRenderer>();
        physicsCollider = GetComponentInParent<BoxCollider2D>();
    }

    public abstract override void Interact();

    public void Open()
    {
        doorSprite.gameObject.SetActive(false);
        physicsCollider.gameObject.SetActive(false);
        isOpen = true;
    }

    public void Close()
    {

    }
}
