using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ITEMS - Making an RPG in Unity (E04)
//https://www.youtube.com/watch?v=HQNl3Ff2Lpo&ab_channel=Brackeys
//7:03

/*
    Andrew Skinner (Retrieved 12/29/2021)
For those of you who were looking to make it so items could stack, 
here is what I did: in the Items script, add a public int called "itemAmount" 
and default it to 1. Inside the Inventory script, in the Add function, 
instead of simply doing items.Add(item), at the top of the function 
you can create Item copyItem = Instantiate(item);. Loop through the existing items, 
and if the item.name that you're adding == an existing one, 
then update the existing item's itemAmount (++ if you're just adding 1), 
and don't run the items.Add(copyItem). 
I found that if i didn't instantiate a copy of the item it actually updated the value of the base item, 
meaning if I had picked up 8 ore already, the next one I picked up had a default value of 8. 
After you've got it working properly, just setup a Text object inside the Inventory Slot prefab and you're good to go!
*/

//Might have to learn about "Singleton Patterns"

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

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

    public int space = 20;

    public List<Item> items = new List<Item>();

    public InventoryKeyItem keyItem;


    #region Manage item
    public bool Add(Item item)
    {
        if (item.itemType == "Clue" || item.itemType == "Evidence")
        {
            keyItem.Add(item);
        }    
        if (!item.isDefaultItem && item.itemType == "Consumable")
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);
            Debug.Log("Added " + item.name);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        Debug.Log("Removed " + item.name);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    #endregion

    public void AddList(Item[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            Add(item[i]);
            Debug.Log("adding item #" + i);
        }
    }

    public void RemoveList(Item[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            Remove(item[i]);
        }
    }
}

