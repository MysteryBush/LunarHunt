using UnityEngine;

//INTERACTION - Making an RPG in Unity (E02)
//https://www.youtube.com/watch?v=9tePzyL6dgc&ab_channel=Brackeys

/*
This component is for all objects that the player can
interact with such as enemies, items etc. It is meant
to be used as a base class.
*/

public class Interactable : MonoBehaviour
{
    public float radius = 3f; // How close do we need to be to interact
    public Transform interactionTransform; // The transform from where we interact
    bool isFocus = false; // Is this interactable currently being focused?
    Transform player; // Reference to the player transform

    bool hasInteracted = false; // Have we already interacted with the object?

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}