using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    private DialogueTrigger _dialogueTrigger;
    private npcMovement _npcMovement;
    private bool firstChat = true;
    private bool chatRange = false;
    private bool chatOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        _dialogueTrigger = GetComponent<DialogueTrigger>();
        _npcMovement = GetComponent<npcMovement>();
    }

    private void Update()
    {
        triggerDialogue();
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //The first implementation as hit once to start dialogue
        
        //if (collision.gameObject.CompareTag("Player") && firstChat == true)
        //{
        //    firstChat = false;
        //    _dialogueTrigger.TriggerDialouge();
        //}

        //The implementation to hit collider to be in range then player must push button to interact

        if (collision.gameObject.CompareTag("Player"))
        {
            chatRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //The implementation to hit collider to be in range then player must push button to interact
        if (collision.gameObject.CompareTag("Player"))
        {
            chatRange = false;
        }
    }

    private void triggerDialogue()
    {
        if (Input.GetKeyDown(KeyCode.E) && chatRange == true)
        {
            _dialogueTrigger.TriggerDialouge();
            chatOpen = true;
            _npcMovement.faceToPlayer();
        }
    }
}
