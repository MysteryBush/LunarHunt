using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;

public class InkTestingScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;
    //Jumping to a particular "scene"
    //https://github.com/inkle/ink/blob/master/Documentation/RunningYourInk.md#jumping-to-a-particular-scene
    public string knotName;

    public TMP_Text textPrefab;
    public Button buttonPrefab;

    //save a previous text as reference
    private string lastText;
    //
    private bool hasResponse;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);
        story.ChoosePathString(knotName);
        //refreshUI();
        runDialogues();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            runDialogues();
        }
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

    void runDialogues()
    {
        eraseUI();
        //TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;

        string text = loadDialogueChunk();

        List<string> tags = story.currentTags;

        if (tags.Count > 0)
        {
            text = "<b>" + tags[0] + "</b> - " + text;
            if (tags[0] == "END")
            {
                Debug.Log("End of Dialogue");
            }
        }

        //storyText.text = text;
        //storyText.transform.SetParent(this.transform, false);
    }

    void responseButtons()
    {
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
        //refreshUI();
        runDialogues();
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
            lastText = text;
        }
        else
        {
            text = lastText;
            responseButtons();
        }
        return text;
    }
}