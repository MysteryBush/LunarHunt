using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;

public class InkManager : MonoBehaviour
{
    //Instance stuff
    public static InkManager ins;
    private GameObject[] inkManagers;

    //Ink Stuff
    public TextAsset inkJSON;
    private Story story;
    public string knotName;

    //Cutscene linking

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ins = this;

        story = new Story(inkJSON.text);
        story.ChoosePathString(knotName);

        loadData();
        //OpenDialogueBox();
    }

    private void OnLevelWasLoaded(int level)
    {
        inkManagers = GameObject.FindGameObjectsWithTag("InkManager");

        if (inkManagers.Length > 1)
        {
            Destroy(inkManagers[1]);
        }

        loadData();
        //OpenDialogueBox();
    }

    private void loadData()
    {
        knotName = FindObjectOfType<SceneData>().knotLocationName;
    }

    void runDialogues()
    {
        string text = loadDialogueChunk();
        foreach (var tag in story.currentTags)
        {
            if (tag.StartsWith("debug"))
            {
                Debug.Log("Debug from Ink");
            }
            if (tag.StartsWith("END"))
            {
                Debug.Log("End of story");
                return;
            }
            if (tag.StartsWith("Cutscene."))
            {
                Debug.Log("Play cutscene");
            }
        }
    }

    #region dialogue functions
    void responseButtons()
    {
        foreach (Choice choice in story.currentChoices)
        {
            {
                chooseStoryChoice(choice);
            }
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
            text = story.ContinueMaximally();
            //Debug.Log("story.Continue(): " + story.Continue());
        }
        else
        {
            responseButtons();
        }
        return text;
    }

    public void runInk()
    {
        //setting knot
        story.ChoosePathString(knotName);
        //run dialogues
        runDialogues();
    }
    #endregion
}
