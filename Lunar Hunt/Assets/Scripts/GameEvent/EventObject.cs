using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/EventObject")]
public class EventObject : ScriptableObject
{
    new public string name = "New Event Object";
    [SerializeField] [TextArea] private string desc;
    [SerializeField] [TextArea] public string[] descList;
    public bool isUniqueObj = true;

    public string Name => name;
    public string Desc => desc;
    public string[] DescList => descList;
}
