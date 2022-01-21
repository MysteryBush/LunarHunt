using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/EventObject")]
public class EventObject : ScriptableObject
{
    new public string name = "New Event Object";
    [SerializeField] [TextArea] private string desc;
    public bool isUniqueObj = true;
}
