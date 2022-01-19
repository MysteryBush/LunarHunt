using UnityEngine;

//ITEMS - Making an RPG in Unity (E04)
//https://www.youtube.com/watch?v=HQNl3Ff2Lpo&ab_channel=Brackeys

//INVENTORY CODE - Making an RPG in Unity (E06)
//https://www.youtube.com/watch?v=YLhj7SfaxSE&ab_channel=Brackeys

[CreateAssetMenu(fileName = "New Clue", menuName = "Inventory/Clue")]
public class Clue : ScriptableObject
{
    new public string name = "New Clue";
    public Sprite icon = null;
    public string clueFact = "Neutral";
    public bool isDefaultItem = false;
    public int itemAmount = 1;
    public virtual void Use()
    {
        // Use the Clue
        // Something might happen

        Debug.Log("Using " + name);
    }
}
