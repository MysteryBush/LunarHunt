using UnityEngine;
using System.Collections.Generic;

public class ClueUI : MonoBehaviour
{
    #region Singleton

    public static ClueUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryUI found!");
            return;
        }
        instance = this;
    }

    #endregion

    public Transform itemsParent;
    public GameObject clueUI;

    InventoryKeyItem clues;

    [SerializeField] InventorySlot[] slots;

    //current case clues
    [SerializeField] private CurrentCase currentCase;

    //ref 
    [SerializeField] private FormEvidence formEvidence;
    void Start()
    {
        clues = InventoryKeyItem.instance;
        clues.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);

        //currentCaseClues
        currentCase = GameObject.Find("GameManager").GetComponent<CurrentCase>();

        //formEvidence = GameObject.Find("CanvasDialogue").GetComponentInChildren<FormEvidence>();

        //when loaded from another scene
        UpdateUI();
    }

    //https://forum.unity.com/threads/the-object-of-type-image-has-been-destroyed-but-you-are-still-trying-to-access-it-your-script-sho.277287/ #14
    private void OnDestroy()
    {
        //EventManager.OnEvent -= SomeMethod;
        clues.onItemChangedCallback -= UpdateUI;
    }

    public void UpdateUI()
    {
        checkCurrentCaseClues();
        for (int i = 0; i < slots.Length; i++)
        {
            //if (i < clues.clues.Count)
            if (i < currentCase.currentCaseClues.Count)
            {
                slots[i].AddClue(currentCase.currentCaseClues[i]);
            }
            else
            {
                slots[i].ClearClueSlot();
            }
        }
        //Debug.Log("UPDATING UI");
    }

    void checkCurrentCaseClues()
    {
        currentCase.currentCaseClues.Clear();
        for (int i = 0; i < clues.clues.Count; i++)
        {
            if (clues.clues[i].caseNumber == currentCase.currentEvidenceRequirement)
            {
                currentCase.currentCaseClues.Add(clues.clues[i]);
            }
        }
    }
    //public void toggleInventory()
    //{
    //    inventoryUI.SetActive(!inventoryUI.activeSelf);
    //}
}
