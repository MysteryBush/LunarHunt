using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/ConversationObject")]
public class ConversationObject : ScriptableObject
{
    //[SerializeField] private SpeakerObject speakerObject;
    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private ConversationObject nextConversation;
    [SerializeField] private Response[] responses;
    [SerializeField] private EventVar[] gameEvents;
    [SerializeField] private Item[] items;

    //public string Speaker => speakerObject.name;
    //public Sprite Portrait => speakerObject.portrait;
    public Dialogue[] Dialogues => dialogue;
    public bool HasNextConversation => nextConversation != null;
    public ConversationObject NextConversation => nextConversation;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
    public bool HasEvents => gameEvents != null && gameEvents.Length > 0;
    public EventVar[] GameEvents => gameEvents;
    public bool HasItem => items != null;
    public Item[] Items => items;

}
