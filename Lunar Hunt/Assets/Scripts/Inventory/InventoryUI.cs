using UnityEngine;

//INVENTORY CODE - Making an RPG in Unity (E06)
//https://www.youtube.com/watch?v=YLhj7SfaxSE&ab_channel=Brackeys

public class InventoryUI : MonoBehaviour
{
    #region Singleton

    public static InventoryUI instance;

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
    public GameObject inventoryUI;
    //for Key Items slots
    public Transform keyItemsParent;

    Inventory inventory;
    InventoryKeyItem inventoryKeyItem;

    InventorySlot[] slots;
    InventorySlot[] keyItemSlots;

    // Use this for initialization
    void Start()
    {

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        inventoryKeyItem = InventoryKeyItem.instance;
        inventoryKeyItem.onItemChangedCallback += UpdateKeyUI;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);
        keyItemSlots = keyItemsParent.GetComponentsInChildren<InventorySlot>(true);

        //when loaded from another scene
        UpdateUI();
        UpdateKeyUI();
    }
    private void OnDestroy()
    {
        //EventManager.OnEvent -= SomeMethod;
        inventory.onItemChangedCallback -= UpdateUI;
        inventoryKeyItem.onItemChangedCallback -= UpdateKeyUI;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Inventory"))
        //{
        //    inventoryUI.SetActive(!inventoryUI.activeSelf);
        //}
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
        //Debug.Log("UPDATING UI");
    }

    void UpdateKeyUI()
    {
        for (int i = 0; i < keyItemSlots.Length; i++)
        {
            if (i < inventoryKeyItem.evidences.Count)
            {
                keyItemSlots[i].AddItem(inventoryKeyItem.evidences[i]);
            }
            if (i < inventoryKeyItem.clues.Count)
            {
                keyItemSlots[i].AddItem(inventoryKeyItem.clues[i]);
            }
            else
            {
                keyItemSlots[i].ClearSlot();
            }
        }
        //Debug.Log("UPDATING KEY UI");
    }

    //public void toggleInventory()
    //{
    //    inventoryUI.SetActive(!inventoryUI.activeSelf);
    //}
}
