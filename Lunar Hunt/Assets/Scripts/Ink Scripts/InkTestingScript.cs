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

    public TMP_Text textPrefab;
    public Button buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);

        refreshUI();
    }

    void refreshUI()
    {
        eraseUI();

        TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;

        //string text = loadStoryChunk();

        //List<string> tags = story.currentTags;

        //if (tags.Count > 0)
        //{
        //    text = "<b>" + tags[0] + " - " + text;
        //} 

        storyText.text = loadStoryChunk();
        storyText.transform.SetParent(this.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();
            choiceText.text = choice.text;
            choiceButton.transform.SetParent(this.transform, false);

            choiceButton.onClick.AddListener(delegate {
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

    // Update is called once per frame
    void Update()
    {

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
}