using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Interactable focus;

    Camera cam; // Reference to our camera
    //PlayerMotor motor; // Reference to our motor

    //public Interactable focus;
    bool isControl = true;

    // Get references
    void Start()
    {
        cam = Camera.main;
        //motor = GetComponent<PlayerMotor>();
    }
    void Update()
    {
        controlInput();
    }

    void controlInput()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            //trigger interaction
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //trigger Inventory
            InventoryUI.instance.toggleInventory();
            Debug.Log("toggling inventory");
        }

        // If we press left mouse
        if (Input.GetMouseButtonDown(0))
        {
            ////Clicking on object to start interacting
            ////We create a ray
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            ////If the ray hits
            //if (Physics.Raycast(ray, out hit, 106))
            //{
            //    Interactable interactable = hit.collider.GetComponent<Interactable>();
            //    if (interactable != null)
            //    {
            //        SetFocus(interactable);
            //    }
            //}
        }
    }
    //void SetFocus(Interactable newFocus)
    //{
    //    if (newFocus != focus)
    //    {
    //        if (focus != null)
    //            focus.OnDefocused();
    //        focus = newFocus;
    //        //motor.FollowTarget(newFocus);
    //    }

    //    focus = newFocus;
    //    newFocus.OnFocused(transform);
    //    //motor.MoveToPoint(newFocus.transform.position);
    //}

    //void RemoveFocus()
    //{
    //    if (focus != null)
    //        focus.OnDefocused();

    //    focus.OnDefocused();
    //    focus = null;
    //}
}