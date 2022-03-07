using UnityEngine;
using System;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private ConversationObject conversationObject;
    [SerializeField] private ResponseEvent[] events;

    public ConversationObject ConversationObject => conversationObject;

    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        if (conversationObject == null) return;
        if (conversationObject.Responses == null) return;
        if (events != null && events.Length == conversationObject.Responses.Length) return;
        
        if (events == null)
        {
            events = new ResponseEvent[conversationObject.Responses.Length];
        }
        else
        {
            Array.Resize(ref events, conversationObject.Responses.Length);
        }

        for (int i = 0; i < conversationObject.Responses.Length; i++)
        {
            Response response = conversationObject.Responses[i];

            if (events[i] != null)
            {
                events[i].name = response.ResponseText;
                continue;
            }
            events[i] = new ResponseEvent() {name = response.ResponseText};
        }
    }
}
