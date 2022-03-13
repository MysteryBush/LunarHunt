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

        inventory = Inventory.instance;
        inventoryKeyItem = InventoryKeyItem.instance;
        inventory.onItemChangedCallback += UpdateUI;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);
    }

    #endregion

    //public delegate void OnChanged();
    //public OnChanged onChangedCallback;

    //public List<InventoryKeyItem> clueForming = new List<InventoryKeyItem>();

    [SerializeField] private ClueUI clueUI;
    //private ResponseEvent[] clueChoices;
    [SerializeField] private InventoryKeyItem inventoryKeyItem;

    [SerializeField] private RectTransform clueBox;
    [SerializeField] private RectTransform clueButtonTemplate;
    [SerializeField] private RectTransform clueContainer;

    //Forming Evidence
    //[SerializeField] public int currentEvidenceRequirement;
    [SerializeField] private CurrentCase currentCase;
    [SerializeField] private List<EvidenceRequirement> evidenceRequirementList;

    Inventory inventory;

    InventorySlot[] slots;

    public List<Item> clues = new List<Item>();
    private List<GameObject> tempClueButtons = new List<GameObject>();

    void Start()
    {
        currentCase = GameObject.Find("GameManager").GetComponent<CurrentCase>();

        //inventory = Inventory.instance;
        //inventoryKeyItem = InventoryKeyItem.instance;
        //inventory.onItemChangedCallback += UpdateUI;

        ////slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        ////slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);
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
            Debug.Log("clue: " + clue);
            int clueIndex = i;

            GameObject clueButton = Instantiate(clueButtonTemplate.gameObject, clueContainer);
            clueButton.gameObject.SetActive(true);
            clueButton.GetComponentInChildren<TMP_Text>().text = clue.name;
            clueButton.GetComponentInChildren<Image>().sprite = clue.icon;
            //clueButton.GetComponent<Button>().onClick.AddListener(call: () => OnPickedResponse(clue, clueIndex));

            tempClueButtons.Add(clueButton);
        }
        //Debug.Log("UPDATING UI");
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
        if (clues.Contains(item) )
        {
            return true;
        }
        else
            return false;
    }

    private Item GetRequirementOutput()
    {
        EvidenceRequirement currentRequirement = evidenceRequirementList[currentCase.currentEvidenceRequirement];
        //Item[] clueRequirement = currentRequirement.clueRequirement;
        //what if the clue is fake? If so, then return null. CANNOT form evidence.
        //Debug.Log(clues.Count);
        for (int x = 0; x < clues.Count; x++)
        {
            if (clues[x].clueFact == "Fake")
            {
                //requirement = false;
                //Debug.Log("FAKE Clue");
                NotifierQueue.ins.notifyFormEvidence(currentRequirement, 0);
                NotifierQueue.ins.NotifyAlert();
                return null;
            }
        }

        //bool requirement = true;
        //foreach (EvidenceRequirement evidenceRequirement in evidenceRequirementList)
        //foreach (Item[] clueRequirement in currentRequirement)
        //{
        for (int i = 0; i < currentRequirement.clueRequirement.Length; i++)
        {
            //when there is required clue
            if (clues.Contains(currentRequirement.clueRequirement[i]))
            {
                //Do nothing
            }
            //what if one of the required clue cannot be found in the inventory, then return null. CANNOT form evidence!
            //else if (!clues.Contains(evidenceRequirement.clueRequirement[i]))
            else
            {
                //requirement = false;
                NotifierQueue.ins.notifyFormEvidence(currentRequirement, 1);
                NotifierQueue.ins.NotifyAlert();
                return null;
            }

        }
        //if (requirement)
        //{
        //}
        return currentRequirement.outputEvidence;
        //}
        //return null;
    }


    public void createEvidence()
    {
        Item evidence = GetRequirementOutput();
        if (!inventoryKeyItem.evidences.Contains(evidence) && evidence != null)
        {
            inventoryKeyItem.Add(evidence);
            Debug.Log("Added evidence: " + evidence);

            //function on what happens when the evidence is formed
            nextFormingEvidence();

            //go to next evidence case


            //evidence.Use();
            NotifierQueue.ins.NotifyAlert();



            //the knotname for Ink
            string knotname = "Evidence_" + evidence.inkName;
            //Debug.Log("Sending Ink's Knot Name: " + knotname);
            //Tell Ink that the evidence is collected
            InkDialogue.ins.runInkKnot(knotname);
        }
    }

    public void nextFormingEvidence()
    {
        currentCase.currentEvidenceRequirement += 1;

        clueUI.UpdateUI();

        clues.Clear();

        UpdateUI();
    }
}