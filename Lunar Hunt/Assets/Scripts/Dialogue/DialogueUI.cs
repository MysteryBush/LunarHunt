using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private GameObject portraitBox;
    public float openTime = 2;
    public Animator anim;
    //find player for playerControl script
    public PlayerControl player;

    public bool IsOpen { get; private set; }

    private Sprite spriteImage;

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;
    //get from other game object
    //public DialogueActivator dialogueactivator;
    public ConversationActivator conversationactivator;

    private void Start()
    {
        IsOpen = true;
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
    }

    public void runConversation(ConversationObject conversationobject)
    {
        dialogueBox.SetActive(true);
        portraitBox.SetActive(true);
        //Let PlayerControl know controlUI = true
        player.controlUI = true;
        //run conversation
        StartCoroutine(routine: StepThroughConversation(conversationobject));
        //animation start dialogue
        anim.SetBool("IsOpen", true);
    }

    public void ShowDialogue(DialogueObject dialogueobject)
    {
        dialogueBox.SetActive(true);
        portraitBox.SetActive(true);
        //Let PlayerControl know controlUI = true
        player.controlUI = true;
        StartCoroutine(routine: StepThroughDialogue(dialogueobject));
        //animation start dialogue
        anim.SetBool("IsOpen", true);
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    //start conversation with this to run each dialogue
    private IEnumerator StepThroughConversation(ConversationObject conversationObject)
    {
        for (int i = 0; i < conversationObject.Dialogue.Length; i++)
        {
            DialogueObject dialogue = conversationObject.Dialogue[i];
            yield return StartCoroutine(runDialogues(dialogue));
        }
        if (conversationObject.HasResponses == true && conversationObject.HasNextConversation == false)
        {
            responseHandler.ShowResponses(conversationObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }
    //run a dialogue
    private IEnumerator runDialogues(DialogueObject dialogueObject)
    {
        textLabel.text = null;
        nameLabel.text = dialogueObject.Speaker;
        spriteImage = dialogueObject.Portrait;
        portraitBox.GetComponent<Image>().sprite = spriteImage;
        if (spriteImage == null)
            portraitBox.SetActive(false);

        if (anim.GetBool("IsOpen") == false)
            yield return new WaitForSeconds(openTime);

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            //break when there are response choices?
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
    }

    //using as ref for this script
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        textLabel.text = null;
        nameLabel.text = dialogueObject.Speaker;
        spriteImage = dialogueObject.Portrait;
        portraitBox.GetComponent<Image>().sprite = spriteImage;
        if (spriteImage == null)
            portraitBox.SetActive(false);

        if (anim.GetBool("IsOpen") == false)
            yield return new WaitForSeconds(openTime);

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            //break when there are response choices?
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            ////break when new dialogue will come next
            //if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasNextDialogue) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        //if (dialogueObject.HasNextDialogue == true)
        //{
        //    dialogueactivator.GetComponent<DialogueActivator>().UpdateDialogueObject(dialogueObject.NextDialogue);
        //    ShowDialogue(dialogueObject.NextDialogue);
        //}
        if (dialogueObject.HasResponses == true && dialogueObject.HasNextDialogue == false)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
        //afterDialogue(dialogueObject);
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }

    private void afterDialogue(DialogueObject dialogueObject)
    {
        if (dialogueObject.HasNextDialogue == true)
        {
            StartCoroutine(routine: StepThroughDialogue(dialogueObject));
        }
        if (dialogueObject.HasResponses && dialogueObject.HasNextDialogue == false)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        player.controlUI = false;
        anim.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        Debug.Log("End dialogue");
    }

    public void findPlayer(PlayerControl playercontrol)
    {
        player = playercontrol;
    }
}
