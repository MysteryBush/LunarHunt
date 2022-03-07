using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/ConversationObject")]
public class ConversationObject : ScriptableObject
{
    //[SerializeField] private SpeakerObject speakerObject;
    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private ConversationObject forceNextConversation;
    [SerializeField] private ConversationObject changeConversation;
    [SerializeField] private Response[] responses;
    [SerializeField] private EventObject[] gameEvents;
    [SerializeField] private Item[] items;
    [SerializeField] private bool skipNotification;

    //public string Speaker => speakerObject.name;
    //public Sprite Portrait => speakerObject.portrait;
    public Dialogue[] Dialogues => dialogue;
    public bool HasForceNextConversation => forceNextConversation != null;
    public ConversationObject ForceNextConversation => forceNextConversation;
    public bool HasChangeConversation => changeConversation != null;
    public ConversationObject ChangeConversation => changeConversation;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
    public bool HasEvents => gameEvents != null && gameEvents.Length > 0;
    public EventObject[] GameEvents => gameEvents;
    public bool HasItem => items != null;
    public Item[] Items => items;
    public bool SkipNotification => skipNotification;

}
