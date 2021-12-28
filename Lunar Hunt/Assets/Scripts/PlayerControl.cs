using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //public Interactable focus;

    bool isControl = true;
    //Camera cam; // Reference to our camera

    void controlInput()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            //trigger dialogue

        }

        //// If we press right mouse
        //if (Input.GetMouseButtonDown(1))
        //{
        //    //Clicking on object to start interacting
        //    // We create a ray
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    // If the ray hits
        //    if (Physics.Raycast(ray, out hit, 106))
        //    {
        //    //    Interactable interactable = hit.collider.GetComponent<Interactable>
        //    //if (interactable != null)
        //    //    {

        //    //    }
        //    }
        //}
    }
}