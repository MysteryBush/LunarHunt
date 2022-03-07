using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormEvidence : MonoBehaviour
{
    //public delegate void OnChanged();
    //public OnChanged onChangedCallback;

    public List<InventoryKeyItem> clueForming = new List<InventoryKeyItem>();

    private ClueUI clueUI;
    private ResponseEvent[] clueChoices;
    [SerializeField] private InventoryKeyItem inventoryKeyItem;

    [SerializeField] private RectTransform clueBox;
    [SerializeField] private RectTransform clueButtonTemplate;
    [SerializeField] private RectTransform clueContainer;

    public static FormEvidence instance;

    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
<<<<<<< HEAD
        return null;
    }


    public void createEvidence()
    {
        Item evidence = GetRequirementOutput();
        if (!inventoryKeyItem.evidences.Contains(evidence))
        {
            inventoryKeyItem.Add(evidence);
            evidence.Use();
            NotifierQueue.instance.NotifyAlert();
        }
=======
        Debug.Log("UPDATING UI");
>>>>>>> parent of 1eb7e33 (Evidence Forming)
    }
}
