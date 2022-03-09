using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryKeyItem : MonoBehaviour
{
    #region Singleton

    public static InventoryKeyItem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> clues = new List<Item>();
    public List<Item> evidences = new List<Item>();

    public bool Add(Item keyItem)
    {
        if (keyItem.isUniqueItem == true && checkInList(keyItem))
        {
            return false;
        }
        if (keyItem.itemType == "Clue")
        {
            clues.Add(keyItem);
        }
        if (keyItem.itemType == "Evidence")
        {
            evidences.Add(keyItem);
        }
        Debug.Log("Added " + keyItem.name);
        NotifierQueue.instance.notifyItem(keyItem);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item keyItem)
    {
        if (keyItem.itemType == "Clue")
        {
            clues.Remove(keyItem);
        }
        if (keyItem.itemType == "Evidence")
        {
            evidences.Remove(keyItem);
        }
        //Debug.Log("Removed " + keyItem.name);
        //NotifierQueue.instance.notifyItem(keyItem);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    private bool checkInList(Item item)
    {
        if (clues.Contains(item) || evidences.Contains(item))
        {
            return true;
        }
        else
            return false;
    }
}
