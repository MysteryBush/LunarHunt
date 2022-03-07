using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogues dialogue;
    private void Start()
    {
        //TriggerDialouge();
    }

    public void TriggerDialouge()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
