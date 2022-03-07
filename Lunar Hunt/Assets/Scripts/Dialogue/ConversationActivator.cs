using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationActivator : MonoBehaviour, IInteractable
{
    [SerializeField] public ConversationObject conversationObject;
    public npcMovement npcMovement;

    private void Start()
    {
        npcMovement = GetComponent<npcMovement>();
    }

    public void UpdateConversationObject(ConversationObject conversationObject)
    {
        this.conversationObject = conversationObject;
    }

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
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.ConversationObject == conversationObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        player.DialogueUI.findPlayer(player);
        player.DialogueUI.conversationactivator = this;
        player.DialogueUI.runConversation(conversationObject);
        npcMovement.targetFacing();
        npcMovement.targetPlayer.GetComponent<PlayerMovement>().targetFacing(npcMovement.npcTransform);
    }
}