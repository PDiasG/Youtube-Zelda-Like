using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Item content;
    public Inventory playerInventory;
    public bool isOpen;
    public CustomSignal raiseItem;
    public GameObject dialogWindow;
    public Text dialogText;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (!isOpen)
        {
            OpenChest();
        }
        else
        {
            CloseChest();
        }
    }

    public override bool ContextClueCondition(Collider2D other)
    {
        return base.ContextClueCondition(other) && !isOpen;
    }

    public void OpenChest()
    {
        dialogWindow.SetActive(true);
        dialogText.text = content.itemDescription;
        playerInventory.AddItem(content);
        playerInventory.currentItem = content;
        raiseItem.Raise();
        contextOff.Raise();
        isOpen = true;
        animator.SetBool("open", true);
    }

    public void CloseChest()
    {
        dialogWindow.SetActive(false);
        raiseItem.Raise();
        animator.SetBool("open", false);
    }
}
