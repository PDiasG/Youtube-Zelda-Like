using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls readable objects in the map
 * Currently only supports single window text, look into more advanced dialogs
 */

public class Signpost : Interactable
{
    public GameObject dialogBox;
    public Text dialogText;
    [TextArea] public string dialog;

    protected new void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.CompareTag("Player"))
        {
            dialogBox.SetActive(false);
        }
    }

    public override void Interact()
    {
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
        else
        {
            dialogBox.SetActive(true);
            dialogText.text = dialog;
        }
    }
}
