using UnityEngine;


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

    InventorySlot[] slots;

    // Use this for initialization
    void Start()
    {
        clues = InventoryKeyItem.instance;
        clues.onItemChangedCallback += UpdateUI;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < clues.clues.Count)
            {
                slots[i].AddClue(clues.clues[i]);
            }
            else
            {
                slots[i].ClearClueSlot();
            }
        }
        Debug.Log("UPDATING UI");
    }

    //public void toggleInventory()
    //{
    //    inventoryUI.SetActive(!inventoryUI.activeSelf);
    //}
}
