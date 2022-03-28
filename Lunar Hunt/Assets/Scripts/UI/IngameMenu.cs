using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameMenu : MonoBehaviour
{
    #region Singleton

    public static IngameMenu ins;

    private void Awake()
    {
        if (ins != null)
        {
            Debug.LogWarning("More than one instance of IngameMenu found!");
            return;
        }
        ins = this;
    }

    #endregion

    public GameObject ingameMenu;
    public Button firstButton;

    [SerializeField] private int initialSection;
    [SerializeField] private GameObject[] sections;
    [SerializeField] private string[] titleName;
    [SerializeField] public TMP_Text sectionText;
    int selectedTab;

    //state to be called on other script
    public bool isOpen;

    //force transitionCanvas to disable itself;
    [SerializeField] private GameObject transitionCanvas;

    private void Start()
    {
        //transitionCanvas = GameObject.Find("CanvasTransition");
        ingameMenu.SetActive(false);
    }

    public void SetAllTabs()
    {
        foreach (GameObject section in sections)
        {
            section.SetActive(false);
        }
    }
    public void selectMenuTab(int tabNumber)
    {
        SetAllTabs();
        sections[tabNumber].SetActive(true);
        sectionText.text = titleName[tabNumber];
    }

    public void toggleIngameMenu()
    {
        //Wait, why do I use initialSection if firstButton is used next anyway?
        selectMenuTab(initialSection);
        firstButton.GetComponent<SwitchButtonsPanelScript>().OnButtonClicked(firstButton);
        ingameMenu.SetActive(!ingameMenu.activeSelf);
        isOpen = ingameMenu.activeSelf;
        //force transitionCanvas to disable itself;
        transitionCanvas.SetActive(false);
    }
}
