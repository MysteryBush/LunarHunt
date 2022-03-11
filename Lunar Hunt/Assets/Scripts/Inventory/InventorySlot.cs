using UnityEngine;
using UnityEngine.UI;
using TMPro;
//INVENTORY CODE - Making an RPG in Unity (E06)
//https://www.youtube.com/watch?v=YLhj7SfaxSE&ab_channel=Brackeys

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TMP_Text text;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.enabled = true;
        icon.sprite = item.icon;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void AddClue(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        text.text = item.name;
    }

    public void ClearClueSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        text.text = null;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
