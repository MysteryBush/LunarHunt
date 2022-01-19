using UnityEngine;

//ITEMS - Making an RPG in Unity (E04)
//https://www.youtube.com/watch?v=HQNl3Ff2Lpo&ab_channel=Brackeys

//INVENTORY CODE - Making an RPG in Unity (E06)
//https://www.youtube.com/watch?v=YLhj7SfaxSE&ab_channel=Brackeys

[CreateAssetMenu(fileName = "New Evidence", menuName = "Inventory/Evidence")]
public class Evidence : ScriptableObject
{
    new public string name = "New Evidence";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int EvidenceAmount = 1;
    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);
    }
}
