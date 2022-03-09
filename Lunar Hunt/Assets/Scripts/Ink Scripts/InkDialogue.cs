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
        player = FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>();
        speakerList = FindObjectOfType<SpeakerList>().gameObject.GetComponent<SpeakerList>();
        addDictionary();

        IsOpen = true;
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();

        story = new Story(inkJSON.text);
        story.ChoosePathString(knotName);
        //test using dialogue without interacting with NPC
        //OpenDialogueBox();
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
        //Speaker List
        speakers = speakerList.speakerObjects;
        speakerPair.Add("Sebastian", speakers[0]);
        speakerPair.Add("Athena", speakers[1]);
        speakerPair.Add("Lumberjack", speakers[2]);
        speakerPair.Add("Merchant", speakers[3]);
        speakerPair.Add("Cassandra", speakers[4]);
        speakerPair.Add("HallStaff", speakers[5]);
        speakerPair.Add("OldMan", speakers[6]);
        speakerPair.Add("Caravan", speakers[7]);
        speakerPair.Add("Villager", speakers[8]);
        //Debug.Log(speakerPair["Sebastian"]);
        //Clue List

        //Evidence List
    }

    void runDialogues()
    {
        //Debug.Log("start run Dialogues");
        string text = loadDialogueChunk();
        //Debug.Log(text);

        //List<string> tags = story.currentTags;
        //if (tags.Count > 0)
        //{
        //    if (tags[0] == "END")
        //    {
        //        //Debug.Log("End of Dialogue");
        //        CloseDialogueBox();
        //        return;
        //    }
        //    //if (tags[0].StartsWith("noSpeaker"))
        //    if (tags.Contains("noSpeaker"))
        //    {
        //        nameLabel.text = null;
        //        portraitBox.GetComponent<Image>().sprite = null;
        //        portraitBox.SetActive(false);
        //    }
        //    if (tags[0].StartsWith("speaker."))
        //    {
        //        //text = "<b>" + tags[0] + "</b>" + text;
        //        var speakerName = tags[0].Substring("speaker.".Length, tags[0].Length - "speaker.".Length);
        //        currentSpeaker = speakerPair[speakerName];
        //        nameLabel.text = currentSpeaker.name;
        //        spriteImage = currentSpeaker.portrait;
        //        portraitBox.GetComponent<Image>().sprite = spriteImage;
        //        portraitBox.SetActive(true);
        //    }
        //    if (tags[0].StartsWith("clue."))
        //    {
        //        var clueName = tags[0].Substring("clue.".Length, tags[0].Length - "clue.".Length);
        //        //add clue to the inventory
        //    }
        //}

        foreach (var tag in story.currentTags)
        {
            if (tag.StartsWith("END"))
            {
                //Debug.Log("End of Dialogue");
                CloseDialogueBox();
                return;
            }
            if (tag.StartsWith("noSpeaker"))
            {
                nameLabel.text = null;
                portraitBox.GetComponent<Image>().sprite = null;
                portraitBox.SetActive(false);
            }
            if (tag.StartsWith("speaker."))
            {
                //text = "<b>" + tags[0] + "</b>" + text;
                var speakerName = tag.Substring("speaker.".Length, tag.Length - "speaker.".Length);
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
                eraseUI();
                chooseStoryChoice(choice);
            });
        }
    }
    void eraseUI()
    {
        textLabel.text = null;
        nameLabel.text = null;
        portraitBox.GetComponent<Image>().sprite = null;
        portraitBox.SetActive(false);
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
            //Debug.Log("story.Continue(): " + story.Continue());
            lastText = text;
        }
        else
        {
            text = lastText;
            if (endDialogue == false)
            {
                responseButtons();
            }
            endDialogue = true;
        }
        return text;
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        player.controlUI = false;
        anim.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        eraseUI();
    }

    public void OpenDialogueBox()
    {
        IsOpen = true;
        player.controlUI = true;
        anim.SetBool("IsOpen", true);
        //setting knot
        story.ChoosePathString(knotName);
        //run dialogues
        endDialogue = false;
        eraseUI();
        runDialogues();
    }
    #endregion

    public void findPlayer(PlayerControl playercontrol)
    {
        player = playercontrol;
    }
}
