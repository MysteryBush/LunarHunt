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

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private GameObject portraitBox;
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
        speakers = speakerList.speakerObjects;
        speakerPair.Add("Sebastian", speakers[0]);
        speakerPair.Add("Athena", speakers[1]);
        speakerPair.Add("Lumberjack", speakers[2]);
        speakerPair.Add("Merchant", speakers[3]);
        Debug.Log(speakerPair["Sebastian"]);

        IsOpen = true;
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
        story = new Story(inkJSON.text);

        runDialogues();
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

    void runDialogues()
    {
        textLabel.text = null;
        nameLabel.text = null;
        spriteImage = currentSpeaker.portrait;
        portraitBox.GetComponent<Image>().sprite = spriteImage;

        //resetUI();

        string text = loadDialogueChunk();

        List<string> tags = story.currentTags;

        if (tags.Count > 0)
        {
            if (tags[0] == "END")
            {
                text = "<b>" + tags[0] + "</b>" + text;
                Debug.Log("End of Dialogue");
            }
            if (tags[0].StartsWith("#speaker."))
            {
                var speakerName = tags[0].Substring("#speaker.".Length, tags[0].Length - "#Clip.".Length);
                currentSpeaker = speakerPair[speakerName];
            }
            //else
            //{
            //    textLabel.text = tags[0];
            //}
        }
        textLabel.text = text;
        nameLabel.text = currentSpeaker.name;
    }

    #region dialogue functions
    void resetUI()
    {
        //for (int i = 0; i < this.transform.childCount; i++)
        //{
        //    Destroy(this.transform.GetChild(i).gameObject);
        //}
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
            text = story.Continue();
        }

        return text;
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        player.controlUI = false;
        anim.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
    #endregion

    public void findPlayer(PlayerControl playercontrol)
    {
        player = playercontrol;
    }

    private void getSpeaker(string speakerName)
    {
        
    }
}
