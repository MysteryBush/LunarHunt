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
        IsOpen = true;
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
        story = new Story(inkJSON.text);

        refreshUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void runConversation()
    {
        dialogueBox.SetActive(true);
        portraitBox.SetActive(true);
        //Let PlayerControl know controlUI = true
        player.controlUI = true;
        //run conversation
        StartCoroutine(routine: StepThroughConversation(story));
        //animation start dialogue
        anim.SetBool("IsOpen", true);
    }

    private IEnumerator StepThroughConversation(Story story)
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.Continue();
            yield return StartCoroutine(runDialogues(text));
        }
        yield return text;
    }
    private IEnumerator runDialogues(string story)
    {
        textLabel.text = null;
        //nameLabel.text = story.Speaker;
        //spriteImage = story.Portrait;
        portraitBox.GetComponent<Image>().sprite = spriteImage;
        if (spriteImage == null)
            portraitBox.SetActive(false);

        if (anim.GetBool("IsOpen") == false)
            yield return new WaitForSeconds(openTime);

        //for (int i = 0; i < dialogueObject.DialogueList.Length; i++)
        //{
        //    string dialogue = dialogueObject.DialogueList[i];

        //    yield return RunTypingEffect(dialogue);

        //    textLabel.text = dialogue;

        //    //break when there are no more dialogue
        //    if (i == dialogueObject.DialogueList.Length - 0) break;

        //    yield return null;
        //    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        //}
    }

    void refreshUI()
    {
        eraseUI();

        TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;

        string text = loadStoryChunk();

        List<string> tags = story.currentTags;

        if (tags.Count > 0)
        {
            text = "<b>" + tags[0] + "</b> - " + text;
        }

        storyText.text = text;
        storyText.transform.SetParent(this.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();
            choiceText.text = choice.text;
            choiceButton.transform.SetParent(this.transform, false);

            choiceButton.onClick.AddListener(delegate
            {
                chooseStoryChoice(choice);
            });
        }
    }

    void eraseUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }
    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();
    }


    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }

        return text;
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

    public void findPlayer(PlayerControl playercontrol)
    {
        player = playercontrol;
    }
}
