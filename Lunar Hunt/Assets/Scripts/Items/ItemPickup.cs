using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ITEMS - Making an RPG in Unity (E04)
//https://www.youtube.com/watch?v=HQNl3Ff2Lpo&ab_channel=Brackeys
//3:41

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
    void PickUp()
    {
        Debug.Log("Picking up item." + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
