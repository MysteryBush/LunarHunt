using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager ins;
    public Text nameText;
    public Text mainText;
    public GameObject portrait;
    Image portraitSprite;
    public Animator anim;
    private Queue<string> sentencesQueue;
    Dialogues setDialogue;
    //private Reward<string> rewardQueue;

    public bool isDone = true;

    // Start is called before the first frame update
    void Awake()
    {
        ins = this;
        portraitSprite = portrait.GetComponent<Image>();
        sentencesQueue = new Queue<string>();
        //rewardQueue = new Reward<string>();
        Debug.Log(sentencesQueue);
    }

    private void FixedUpdate()
    {
        DisplayControl();
    }

    public void StartDialogue(Dialogues _dialogue)
    {
        setDialogue = _dialogue;
        isDone = false;

        Debug.Log("Starting conversation with " + _dialogue.name);

        anim.SetBool("IsOpen", true);

        nameText.text = _dialogue.name;

        portrait.GetComponent<Image>().sprite = _dialogue.portrait;

        sentencesQueue.Clear();

        foreach (string sentence in setDialogue.sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentencesQueue.Count == 0)
        {
            GiveReward();
            EndDialogue();
            anim.SetBool("IsOpen", false);
            isDone = true;
            return;
        }

        string outSentence = sentencesQueue.Dequeue();
        mainText.text = outSentence;

        Debug.Log(outSentence);
    }

    public void GiveReward()
    {
        //if (rewardQueue.Count == 0)
        //{

        //}
        if (isDone == false)
        {
            InventoryManager.potionCount = InventoryManager.potionCount + setDialogue.reward;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }

    void DisplayControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDone == false)
        //if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
}
