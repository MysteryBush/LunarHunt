using UnityEngine;

//ITEMS - Making an RPG in Unity (E04)
//https://www.youtube.com/watch?v=HQNl3Ff2Lpo&ab_channel=Brackeys


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

//INVENTORY CODE - Making an RPG in Unity (E06)
//https://www.youtube.com/watch?v=YLhj7SfaxSE&ab_channel=Brackeys

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int itemAmount = 1;
    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);
    }
}
