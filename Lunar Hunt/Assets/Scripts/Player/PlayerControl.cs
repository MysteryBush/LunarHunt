using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Interactable focus;
    public bool isControl = true;
    public bool doingAction = false;
    public bool controlUI = false;
    public bool isDead = false;

    //dialogue stuff
    [SerializeField] public DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    void Update()
    {
        controlInput();
        toggleControl();
        //GetComponent<PlayerMovement>().isControl = isControl;
    }

    void controlInput()
    {
        if (dialogueUI.IsOpen) return;
        if (isControl == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && controlUI == false)
            {
                if (dialogueUI.chatRange == true)
                    //trigger interaction
                    Interactable?.Interact(player: this);
            }
            if (Input.GetKeyDown(KeyCode.Tab) && DialogueUI.IsOpen == false)
            {
                //trigger Inventory
                IngameMenu.instance.toggleIngameMenu();
                //Debug.Log("toggling In-game Menu");
                //doingAction = true;
                //doingAction = IngameMenu.instance.ingameMenu.activeSelf;
                controlUI = IngameMenu.instance.isOpen;
            }
        }
    }

    public void toggleControl()
    {
        //if (DialogueManager.ins.isDone == true && isDead == false)
        if (isDead == false)
        {
            isControl = true;
            GetComponent<PlayerMovement>().isFacingTarget = false;
        }
        else
        {
            isControl = false;
        }
    }
}