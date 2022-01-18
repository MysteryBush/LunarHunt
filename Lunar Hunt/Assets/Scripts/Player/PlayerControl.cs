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
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    private Rigidbody2D rb;

    void Start()
    {

    }
    void Update()
    {
        controlInput();
        toggleControl();
        //GetComponent<PlayerMovement>().isControl = isControl;
    }

    void controlInput()
    {
        if (dialogueUI.IsOpen) return;
        if (isControl == true && controlUI == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //trigger interaction
                Interactable?.Interact(player: this);
                Debug.Log("Start dialogue");
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                //trigger Inventory
                IngameMenu.instance.toggleIngameMenu();
                Debug.Log("toggling In-game Menu");
                //doingAction = true;
                //doingAction = IngameMenu.instance.ingameMenu.activeSelf;
                controlUI = IngameMenu.instance.isOpen;
            }
            //if (Input.GetKeyDown(KeyCode.Mouse0))
            //{
            //    GetComponent<PlayerCombat>().inputAttack = true;
            //}
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