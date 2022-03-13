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

    //ink stuff
    [SerializeField] public InkDialogue inkUI;
    public InkDialogue InkUI => inkUI;

    [SerializeField] private IngameMenu ingameMenu;

    public IInteractable Interactable { get; set; }
    private void Start()
    {
        //inkUI = InkDialogue.ins.GetComponent<InkDialogue>();
        //findInkUI();
        inkUI = GameObject.Find("CanvasDialogue").GetComponent<InkDialogue>();
        ingameMenu = FindObjectOfType<IngameMenu>();
    }
    void Update()
    {
        controlInput();
        toggleControl();
        //GetComponent<PlayerMovement>().isControl = isControl;
    }

    void controlInput()
    {
        if (inkUI.IsOpen) return;
        if (isControl == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && controlUI == false)
            {
                if (inkUI.chatRange == true)
                    //trigger interaction
                    Interactable?.Interact(player: this);
            }
            if (Input.GetKeyDown(KeyCode.Tab) && InkUI.IsOpen == false && NotifierUI.ins.IsOpen == false)
            {
                //trigger Inventory
                IngameMenu.ins.toggleIngameMenu();
                //Debug.Log("toggling In-game Menu");
                //doingAction = true;
                //doingAction = IngameMenu.instance.ingameMenu.activeSelf;
                controlUI = IngameMenu.ins.isOpen;
            }
        }
    }

    public void toggleControl()
    {
        //if (DialogueManager.ins.isDone == true && isDead == false)
        //if (isDead == false)
        //{
        //    isControl = true;
        //    GetComponent<PlayerMovement>().isFacingTarget = false;
        //}
        //else
        //{
        //    isControl = false;
        //}
    }

    public void findInkUI(InkDialogue inkDialogue)
    {
        inkUI = inkDialogue;
    }
}