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
    #region Conversation and Dialogue
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


    //start conversation with this to run each dialogue
    private IEnumerator StepThroughConversation(ConversationObject conversationObject)
    {
        for (int i = 0; i < conversationObject.Dialogue.Length; i++)
        {
            Dialogue dialogue = conversationObject.Dialogue[i];
            yield return StartCoroutine(runDialogues(dialogue));
        }
        if (conversationObject.HasNextConversation == true)
        {
            conversationactivator.UpdateConversationObject(conversationObject.NextConversation);
        }
        if (conversationObject.HasItem == true)
        {
            Inventory.instance.AddList(conversationObject.Items);
        }
        if (conversationObject.HasResponses == true && conversationObject.HasNextConversation == false)
        {
            responseHandler.ShowResponses(conversationObject.Responses);
        }
        //if (conversationObject.HasClue == true)
        //{
        //    Inventory.instance.Add(conversationObject.Item);
        //}
        //if (conversationObject.HasEvidence == true)
        //{
        //    Inventory.instance.Add(conversationObject.Item);
        //}
        else
        {
            CloseDialogueBox();
        }
    }
    //run a dialogue
    private IEnumerator runDialogues(Dialogue dialogueObject)
    {
        textLabel.text = null;
        nameLabel.text = dialogueObject.Speaker;
        spriteImage = dialogueObject.Portrait;
        portraitBox.GetComponent<Image>().sprite = spriteImage;
        if (spriteImage == null)
            portraitBox.SetActive(false);

        if (anim.GetBool("IsOpen") == false)
            yield return new WaitForSeconds(openTime);

        for (int i = 0; i < dialogueObject.DialogueList.Length; i++)
        {
            string dialogue = dialogueObject.DialogueList[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            //break when there are no more dialogue
            if (i == dialogueObject.DialogueList.Length - 0) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
    }

    #endregion

    #region ref
    //using as ref for this script

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
    #endregion

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

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        player.controlUI = false;
        anim.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void findPlayer(PlayerControl playercontrol)
    {
        player = playercontrol;
    }
}
