using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FormEvidence : MonoBehaviour
{
    #region Singleton
    public static FormEvidence instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EventTracking found!");
            return;
        }
        instance = this;
    }

    #endregion

    //public delegate void OnChanged();
    //public OnChanged onChangedCallback;

    //public List<InventoryKeyItem> clueForming = new List<InventoryKeyItem>();

    private ClueUI clueUI;
    //private ResponseEvent[] clueChoices;
    [SerializeField] private InventoryKeyItem inventoryKeyItem;

    [SerializeField] private RectTransform clueBox;
    [SerializeField] private RectTransform clueButtonTemplate;
    [SerializeField] private RectTransform clueContainer;

    //Forming Evidence
    [SerializeField] private List<EvidenceRequirement> evidenceRequirementList;

    Inventory inventory;

    InventorySlot[] slots;

    public List<Item> clues = new List<Item>();
    private List<GameObject> tempClueButtons = new List<GameObject>();

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);
    }

    void UpdateUI()
    {
        ////destroy choices button
        foreach (GameObject button in tempClueButtons)
        {
            Destroy(button);
        }
        tempClueButtons.Clear();

        for (int i = 0; i < clues.Count; i++)
        {
            Item clue = clues[i];
            int clueIndex = i;

            GameObject clueButton = Instantiate(clueButtonTemplate.gameObject, clueContainer);
            clueButton.gameObject.SetActive(true);
            clueButton.GetComponentInChildren<TMP_Text>().text = clue.name;
            clueButton.GetComponentInChildren<Image>().sprite = clue.icon;
            //clueButton.GetComponent<Button>().onClick.AddListener(call: () => OnPickedResponse(clue, clueIndex));

            tempClueButtons.Add(clueButton);
        }
        Debug.Log("UPDATING UI");
    }

    public bool toggleSelect(Item item)
    {
        if (checkInList(item))
        {
            clues.Remove(item);
        }
        else
        {
            if (clues.Count <= 3)
                clues.Add(item);
        }
        UpdateUI();
        return true;
    }

    private bool checkInList(Item item)
    {
        if (clues.Contains(item))
        {
            return true;
        }
        else
            return false;
    }

    private Item GetRequirementOutput()
    {
        foreach (EvidenceRequirement evidenceRequirement in evidenceRequirementList)
        {
            bool requirement = true;
            for (int i = 0; i < evidenceRequirement.clueRequirement.Length; i++)
            {
                //when there is required clue
                if (clues.Contains(evidenceRequirement.clueRequirement[i]))
                {
                    //what if there are no required clue at all?
                    if (!clues.Contains(evidenceRequirement.clueRequirement[i]))
                        requirement = false;
                    //what if the clue is fake?
                    for (int x = 0; x < clues.Count; x++)
                    {
                        if (clues[x].itemType == "fake") requirement = false;
                    }
                }
            }
            if (requirement)
            {
                return evidenceRequirement.outputEvidence;
            }
        }
        return null;
    }


    public void createEvidence()
    {
        Item evidence = GetRequirementOutput();
        if (!inventoryKeyItem.evidences.Contains(evidence))
        {
            inventoryKeyItem.Add(evidence);
            NotifierQueue.instance.NotifyAlert();
        }
    }
}