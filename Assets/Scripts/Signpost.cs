using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signpost : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;

    InputMaster inputMaster;
    void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Interaction.performed += ctx => ReadSignpost();
    }

    private void OnEnable()
    {
        inputMaster.Player.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Player.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    void ReadSignpost()
    {
        if (playerInRange)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
