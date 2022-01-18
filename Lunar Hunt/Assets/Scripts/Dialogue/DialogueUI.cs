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

    private void Start()
    {
        IsOpen = true;
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueobject, PlayerControl playercontrol)
    {
        player = playercontrol;
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

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
        //Does this CloseDialogueBox() needed here?
        //CloseDialogueBox();
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

    public void CloseDialogueBox()
    {
        IsOpen = false;
        player.controlUI = false;
        anim.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        Debug.Log("End dialogue");
    }
}
