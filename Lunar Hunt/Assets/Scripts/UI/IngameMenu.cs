using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour
{
    #region Singleton

    public static IngameMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of IngameMenu found!");
            return;
        }
        instance = this;
    }

    #endregion

    public GameObject ingameMenu;
    public Button firstButton;

    [SerializeField]
    private GameObject[] sections;
    int selectedTab;

    //state to be called on other script
    public bool isOpen;

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
    }

    public void toggleIngameMenu()
    {
        selectMenuTab(0);
        firstButton.GetComponent<SwitchButtonsPanelScript>().OnButtonClicked(firstButton);
        ingameMenu.SetActive(!ingameMenu.activeSelf);
        isOpen = ingameMenu.activeSelf;
    }
}
