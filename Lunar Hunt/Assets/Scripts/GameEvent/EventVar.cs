using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Event Variable")]
public class EventVar : ScriptableObject
{
    new public string name = "New Event Variable";
    [SerializeField] private string eventVar;
    public int varAmount = 0;

    public virtual void Trigger()
    {

    }
}
