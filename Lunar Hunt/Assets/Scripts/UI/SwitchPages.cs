using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchPages : MonoBehaviour
{
    public int currentPage;
    public GameObject pageParent;
    [SerializeField] public GameObject[] pages;
    [SerializeField] public string[] pageName;


    //Text ref
    public TMP_Text pageTitle;

    //Buttons ref
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject previousButton;
    [SerializeField] GameObject endButton;

    private void Start()
    {
        showPage();
    }

    public void showPage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        pages[currentPage].SetActive(true);
        pageTitle.text = pageName[currentPage];
        checkInteractable();
    }

    public void nextPage()
    {
        currentPage += 1;
        showPage();
    }

    public void previousPage()
    {
        currentPage -= 1;
        showPage();
    }

    public void checkInteractable()
    {
        //if reach the last page
        if (currentPage == pages.Length - 1)
        {
            //nextPage disable
            nextButton.SetActive(false);
            endButton.SetActive(true);
        }
        //if at the first page
        else if (currentPage == 0)
        {
            //previousPage disable
            previousButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
            previousButton.SetActive(true);
            endButton.SetActive(false);
        }
    }
}
