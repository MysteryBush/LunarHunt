using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    //[SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private ConversationObject conversationObject;
    [SerializeField] private EventObject requireEvent;

    public string ResponseText => responseText;

    public ConversationObject ConversationObject => conversationObject;

    public EventObject RequireEvent => requireEvent;
}
