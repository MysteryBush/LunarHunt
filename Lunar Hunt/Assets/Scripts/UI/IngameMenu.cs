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

    [SerializeField]
    private GameObject[] sections;
    [SerializeField] private string[] titleName;
    [SerializeField] public TMP_Text sectionText;
    int selectedTab;

    //state to be called on other script
    public bool isOpen;

    private void Start()
    {
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
        selectMenuTab(0);
        firstButton.GetComponent<SwitchButtonsPanelScript>().OnButtonClicked(firstButton);
        ingameMenu.SetActive(!ingameMenu.activeSelf);
        isOpen = ingameMenu.activeSelf;
    }
}
