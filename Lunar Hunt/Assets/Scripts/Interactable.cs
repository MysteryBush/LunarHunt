using UnityEngine;

//INTERACTION - Making an RPG in Unity (E02)
//https://www.youtube.com/watch?v=9tePzyL6dgc&ab_channel=Brackeys

//ITEMS - Making an RPG in Unity (E04)
//https://www.youtube.com/watch?v=HQNl3Ff2Lpo&ab_channel=Brackeys

/*
This component is for all objects that the player can
interact with such as enemies, items etc. It is meant
to be used as a base class.
*/

//Also using "DialogueInteraction" as a reference to write this code

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



    void Update()
    {
        //if (isFocus && !hasInteracted)
        //{
        //    float distance = Vector2.Distance(player.position, interactionTransform.position);
        //    if (distance <= radius)
        //    {
        //        Interact();
        //        hasInteracted = true;
        //        isFocus = false;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.E) && isFocus == true && !hasInteracted)
        {
            //float distance = Vector2.Distance(player.position, interactionTransform.position);
            //if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
                isFocus = false;
            }
        }

    }

    //public void OnFocused(Transform playerTransform)
    //{
    //    isFocus = true;
    //    player = playerTransform;
    //    hasInteracted = false;
    //}

    //public void OnDefocused()
    //{
    //    isFocus = false;
    //    player = null;
    //    hasInteracted = false;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //The implementation to hit collider to be in range then player must push button to interact

        if (collision.gameObject.CompareTag("Player"))
        {
            isFocus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //The implementation to hit collider to be in range then player must push button to interact
        if (collision.gameObject.CompareTag("Player"))
        {
            isFocus = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}