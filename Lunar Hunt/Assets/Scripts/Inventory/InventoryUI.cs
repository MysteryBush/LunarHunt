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

    Inventory inventory;

    InventorySlot[] slots;

    // Use this for initialization
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
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
        Debug.Log("UPDATING UI");
    }

    public void toggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
