using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class NotifierUI : MonoBehaviour
{
    #region Singleton
    public static NotifierUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EventTracking found!");
            return;
        }
        instance = this;
    }

    #endregion
    [SerializeField] private GameObject notificationBox;
    [SerializeField] private TMP_Text textLabel;
    public float openTime = 2;
    public Animator anim;
    //find player for playerControl script
    public PlayerControl player;
    public NotifierQueue notifierQueue;

    public bool IsOpen { get; private set; }
    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>();
        IsOpen = true;
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseNotifierBox();
    }
    #region Conversation and Dialogue
    public void runNotifications(NotifierQueue notifierQueue)
    {
        notificationBox.SetActive(true);
        //Let PlayerControl know controlUI = true
        player.controlUI = true;
        //run conversation
        StartCoroutine(routine: StepThroughNotifications(notifierQueue));
        //animation start dialogue
        anim.SetBool("IsOpen", true);
    }
    //start notification on each of them
    private IEnumerator StepThroughNotifications(NotifierQueue notifierQueue)
    {
        //for (int i = 0; i < notifierQueue.notifierList.Count; i++)
        //{
        //    Notifier notifer = notifierQueue.notifierList[i];
        //    //notifierQueue.Remove(notifierQueue.notifierList[0]);
        //    yield return StartCoroutine(runNotifer(notifer));
        //}
        while (notifierQueue.notifierList.Count > 0)
        {
            Notifier notifer = notifierQueue.notifierList[0];
            notifierQueue.Remove(notifierQueue.notifierList[0]);
            yield return StartCoroutine(runNotifer(notifer));
        }
        CloseNotifierBox();
    }
    //run a notification
    private IEnumerator runNotifer(Notifier notifier)
    {
        textLabel.text = null;

        if (anim.GetBool("IsOpen") == false)
            yield return new WaitForSeconds(openTime);

        for (int i = 0; i < notifier.NotifyDesc.Length; i++)
        {
            string notification = notifier.NotifyDesc[i];

            yield return RunTypingEffect(notification);

            textLabel.text = notification;

            //break when there are no more dialogue
            if (i == notifier.NotifyDesc.Length - 0) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
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

    public void CloseNotifierBox()
    {
        IsOpen = false;
        //check if other UI is closed then make player move
        if (player.InkUI.IsOpen == false)
        {
            player.controlUI = false;
        }
        anim.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        //continueDialogue();
    }

    public void findPlayer(PlayerControl playercontrol)
    {
        player = playercontrol;
    }

    private void continueDialogue()
    {
        //if (player.DialogueUI.conversationactivator == null)
        //    return;
        //player.DialogueUI.afterDialogue(player.DialogueUI.conversationactivator.conversationObject);
    }
}
