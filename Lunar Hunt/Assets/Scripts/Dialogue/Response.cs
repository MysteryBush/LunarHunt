using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    //[SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private ConversationObject conversationObject;
    [SerializeField] private EventObject requireEvent;
    [SerializeField] private Item requireItem;

    public string ResponseText => responseText;

    public ConversationObject ConversationObject => conversationObject;

    public EventObject RequireEvent => requireEvent;
    public Item RequireItem => requireItem;
}
