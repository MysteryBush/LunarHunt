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
        if (isControl == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //trigger interaction
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
        if (DialogueManager.ins.isDone == true && isDead == false)
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