using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    //[SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private string knotName;

    //public void UpdateDialogueObject(DialogueObject dialogueObject)
    //{
    //    this.dialogueObject = dialogueObject;
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerControl player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerControl player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }
    public void Interact(PlayerControl player)
    {
        //foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        //{
        //    if (responseEvents.ConversationObject == dialogueObject)
        //    {
        //        player.inkUI.AddResponseEvents(responseEvents.Events);
        //        break;
        //    }
        //}
        //player.DialogueUI.dialogueactivator = this;
        //player.InkUI.getPlayer(player);
        player.InkUI.knotName = knotName;
        player.InkUI.OpenDialogueBox();
    }
}