using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    //[SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private ConversationObject conversationObject;

    public string ResponseText => responseText;

    public ConversationObject ConversationObject => conversationObject;
}
