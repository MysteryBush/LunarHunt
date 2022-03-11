using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class InkDialogue : MonoBehaviour
{
    public static InkDialogue ins;

    //for don't destroy on load
    private GameObject[] inkDialogues;

    public TextAsset inkJSON;
    private Story story;

    //Jumping to a particular "scene"
    //https://github.com/inkle/ink/blob/master/Documentation/RunningYourInk.md#jumping-to-a-particular-scene
    public string knotName;

    #region DICTIONARY
    //DICTIONARY
    //taking data reference
    //speaker data
    public SpeakerList speakerList;
    private List<SpeakerObject> speakers;
    private Dictionary<string, SpeakerObject> speakerPair = new Dictionary<string, SpeakerObject>();
    private SpeakerObject currentSpeaker;
    //private SpeakerObject speakerObject;

    //clue data 
    public ClueList clueList;
    private List<Item> clues;
    private Dictionary<string, Item> cluePair = new Dictionary<string, Item>();
    private Item collectedClue;

    public TMP_Text textPrefab;
    public Button buttonPrefab;

    //timeline data
    public TimelineList timelineList;
    private List<TimelineAsset> timelines;
    private Dictionary<string, TimelineAsset> timelinePair = new Dictionary<string, TimelineAsset>();
    private TimelineAsset useTimeline;

    #endregion
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

    //track if notiferbox is open or not
    public bool notifyIsOpen;

    private Sprite spriteImage;

    private TypewriterEffect typewriterEffect;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ins = this;

        //sceneData give initial name
        knotName = SceneData.ins.initialKnot;

        player = FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>();
        speakerList = SpeakerList.instance;
        clueList = ClueList.instance;
        timelineList = TimelineList.instance;

        addDictionary();

        IsOpen = true;
        notifyIsOpen = false;
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();

        story = new Story(inkJSON.text);
        story.ChoosePathString(knotName);
        //test using dialogue without interacting with NPC
        //OpenDialogueBox();

        player.findInkUI();

        if (knotName != "") OpenDialogueBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.controlUI == true && notifyIsOpen == false)
        {
            runDialogues();
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        inkDialogues = GameObject.FindGameObjectsWithTag("InkDialogue");
        player = FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>();
        speakerList = SpeakerList.instance;
        clueList = ClueList.instance;

        if (inkDialogues.Length > 1)
        {
            Destroy(inkDialogues[1]);
        }

        player.findInkUI();
    }

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
        clues = clueList.clueItems;
        cluePair.Add("Clue_01", clues[0]);
        cluePair.Add("Clue_02", clues[1]);
        cluePair.Add("Clue_03", clues[2]);
        cluePair.Add("Clue_04", clues[3]);
        cluePair.Add("Clue_05", clues[4]);
        cluePair.Add("Clue_06", clues[5]);
        cluePair.Add("News_about_Moving_Out_People", clues[6]);
        cluePair.Add("Visitors_do_not_Checkout", clues[7]);
        cluePair.Add("Merchant_Sells_the_Newspaper", clues[8]);
        cluePair.Add("CS_Order_to_Forge_The_News", clues[9]);
        cluePair.Add("CS_Sells_the_potion", clues[10]);
        cluePair.Add("This_Potion_Improve_Health", clues[11]);
        cluePair.Add("The_Potion_is_just_Colored_Water", clues[12]);
        cluePair.Add("Cassandra_Prophesied_The_Plague", clues[13]);
        cluePair.Add("No_Record_of_Recent_Plague", clues[14]);
        cluePair.Add("CS_letters", clues[15]);

        //Evidence List
        //CS_Faked_The_News

        //Timeline List
        timelines = timelineList.timelineObjects;
        //timelinePair.Add("", timelines[]);
        timelinePair.Add("Lumberjack_clear_the_path", timelines[1]);
        timelinePair.Add("Lumberjack_go_to_the_Log", timelines[4]);
        timelinePair.Add("Athena", timelines[5]);
    }

    void runDialogues()
    {
        //Debug.Log("runDialogues: " + notifyIsOpen);
        //Debug.Log("start run Dialogues");
        string text = loadDialogueChunk();
        //Debug.Log(text);

        foreach (var tag in story.currentTags)
        {
            if (tag.StartsWith("debug."))
            {
                var debugLog = tag.Substring("debug.".Length, tag.Length - "debug.".Length);
                Debug.Log("INK DEBUG: " + debugLog);
            }
            if (tag.StartsWith("END"))
            {
                //Debug.Log("End of Dialogue");
                CloseDialogueBox();
                return;
            }
            if (tag.StartsWith("OPEN"))
            {
                OpenDialogueBox();
                return;
            }    
            if (tag.StartsWith("noSpeaker"))
            {
                //nameLabel.text = null;
                //portraitBox.GetComponent<Image>().sprite = null;
                //portraitBox.SetActive(false);
                currentSpeaker = speakerList.narratorObject;
                nameLabel.text = currentSpeaker.name;
                spriteImage = currentSpeaker.portrait;
                portraitBox.GetComponent<Image>().sprite = spriteImage;
                portraitBox.SetActive(true);
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
            if (tag.StartsWith("clue."))
            {
                //Debug.Log("start get clue: " + notifyIsOpen);
                var clueName = tag.Substring("clue.".Length, tag.Length - "clue.".Length);
                collectedClue = cluePair[clueName];
                //add clue to the inventory
                Inventory.instance.Add(collectedClue);
                //Debug.Log("reached clue with: " + notifyIsOpen);
            }
            if (tag.StartsWith("timeline."))
            {
                var timelineName = tag.Substring("timeline.".Length, tag.Length - "timeline.".Length);
                useTimeline = timelinePair[timelineName];
                //Debug.Log(CutsceneTrigger.instance);
                //Debug.Log("useTimeline: " + useTimeline);
                CutsceneTrigger.instance.GetCutscene(useTimeline);
                CutsceneTrigger.instance.TriggerCutscene();
                //Debug.Log("play cutscene");
            }
            if (tag.StartsWith("knot."))
            {
                var changeToKnot = tag.Substring("knot.".Length, tag.Length - "knot.".Length);
                knotName = changeToKnot;
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
        //Debug.Log("eraseUI: " + notifyIsOpen);
        textLabel.text = null;
        currentSpeaker = speakerList.narratorObject;
        nameLabel.text = currentSpeaker.name;
        spriteImage = currentSpeaker.portrait;
        portraitBox.GetComponent<Image>().sprite = spriteImage;
        portraitBox.SetActive(false);
        for (int i = 0; i < responseBox.transform.childCount; i++)
        {
            Destroy(responseBox.transform.GetChild(i).gameObject);
        }
    }
    void chooseStoryChoice(Choice choice)
    {
        //Debug.Log("chooseStoryChoice: " + notifyIsOpen);
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
        //Debug.Log(IsOpen);
        //check if NotifierUI is closed then make player move
        if (notifyIsOpen == false)
        {
            player.controlUI = false;
            player.GetComponent<PlayerMovement>().isFacingTarget = false;
        }
        anim.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        //Debug.Log(anim.GetBool("IsOpen"));
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

    //run only Ink just to change variables in Ink
    public void runInkKnot(string knot)
    {
        knotName = knot;
        //setting knot
        story.ChoosePathString(knotName);
        runDialogues();
    }

    public void findPlayer(PlayerControl playercontrol)
    {
        player = playercontrol;
    }

    //use on Signal Emitter
    //public void changeKnot(string knot)
    //{
    //    knotName = knot;
    //}
    //public void triggerDialogue()
    //{
    //    //changeKnot(knot);
    //    OpenDialogueBox();
    //}
}
