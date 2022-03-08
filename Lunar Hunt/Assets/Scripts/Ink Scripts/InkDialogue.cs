using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;

public class InkDialogue : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    //Jumping to a particular "scene"
    //https://github.com/inkle/ink/blob/master/Documentation/RunningYourInk.md#jumping-to-a-particular-scene
    public string knotName;

    //taking data reference
    public SpeakerList speakerList;
    private List<SpeakerObject> speakers;
    private Dictionary<string, SpeakerObject> speakerPair = new Dictionary<string, SpeakerObject>();
    private SpeakerObject currentSpeaker;
    //private SpeakerObject speakerObject;


    public TMP_Text textPrefab;
    public Button buttonPrefab;

    //save a previous text as reference
    private string lastText;
    //the state of Ink Dialogue
    private bool endDialogue;

    //object reference
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private GameObject portraitBox;
    [SerializeField] private GameObject responseBox;

    //Animation
    public float openTime = 2;
    public Animator anim;
    //find player for playerControl script
    public PlayerControl player;
    //Is player in the chatRange?
    public bool chatRange = false;

    public bool IsOpen { get; private set; }

    private Sprite spriteImage;

    private TypewriterEffect typewriterEffect;

    // Start is called before the first frame update
    void Start()
    {
        addDictionary();

        IsOpen = true;
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();

        story = new Story(inkJSON.text);
        story.ChoosePathString(knotName);
        //test using dialogue without interacting with NPC
        OpenDialogueBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            runDialogues();
        }
    }
    //public void runConversation()
    //{
    //    dialogueBox.SetActive(true);
    //    portraitBox.SetActive(true);
    //    //Let PlayerControl know controlUI = true
    //    player.controlUI = true;
    //    //run conversation
    //    StartCoroutine(routine: StepThroughConversation(story));
    //    //animation start dialogue
    //    anim.SetBool("IsOpen", true);
    //}

    void addDictionary()
    {
        speakers = speakerList.speakerObjects;
        speakerPair.Add("Sebastian", speakers[0]);
        speakerPair.Add("Athena", speakers[1]);
        speakerPair.Add("Lumberjack", speakers[2]);
        speakerPair.Add("Merchant", speakers[3]);
        //Debug.Log(speakerPair["Sebastian"]);
    }

    void runDialogues()
    {
        textLabel.text = null;
        nameLabel.text = null;
        //portraitBox.SetActive(false);

        string text = loadDialogueChunk();

        List<string> tags = story.currentTags;
        Debug.Log(story.currentTags);
        if (tags.Count > 0)
        {
            if (tags[0] == "END")
            {
                text = "<b>" + tags[0] + "</b>" + text;
                Debug.Log("End of Dialogue");
                CloseDialogueBox();
                return;
            }
            if (tags[0].StartsWith("speaker."))
            {
                var speakerName = tags[0].Substring("speaker.".Length, tags[0].Length - "speaker.".Length);
                currentSpeaker = speakerPair[speakerName];
                nameLabel.text = currentSpeaker.name;
                spriteImage = currentSpeaker.portrait;
                portraitBox.GetComponent<Image>().sprite = spriteImage;
                portraitBox.SetActive(true);
            }
        }
        textLabel.text = text;
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

    #region dialogue functions
    void responseButtons()
    {
        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();
            choiceText.text = choice.text;
            choiceButton.transform.SetParent(responseBox.transform, false);

            choiceButton.onClick.AddListener(delegate
            {
                chooseStoryChoice(choice);
                eraseResponses();
            });
        }
    }
    void eraseResponses()
    {
        for (int i = 0; i < responseBox.transform.childCount; i++)
        {
            Destroy(responseBox.transform.GetChild(i).gameObject);
        }
    }
    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        runDialogues();
    }

    string loadDialogueChunk()
    {
        string text = "";

        if (story.canContinue)
        {
            endDialogue = false;
            text = story.Continue();
            lastText = text;
        }
        else
        {
            if (endDialogue == false)
            {
                responseButtons();
            }
            text = lastText;
            endDialogue = true;
        }
        return text;
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        //player.controlUI = false;
        anim.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void OpenDialogueBox()
    {
        IsOpen = true;
        anim.SetBool("IsOpen", true);
        runDialogues();
    }
    #endregion

    public void findPlayer(PlayerControl playercontrol)
    {
        player = playercontrol;
    }
}
