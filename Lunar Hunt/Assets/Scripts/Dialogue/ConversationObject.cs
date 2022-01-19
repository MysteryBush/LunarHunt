using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/ConversationObject")]
public class ConversationObject : ScriptableObject
{
    //[SerializeField] private SpeakerObject speakerObject;
    [SerializeField] private DialogueObject[] dialogue;
    [SerializeField] private ConversationObject nextConversation;
    [SerializeField] private Response[] responses;

    //public string Speaker => speakerObject.name;
    //public Sprite Portrait => speakerObject.portrait;
    public DialogueObject[] Dialogue => dialogue;
    public bool HasNextConversation => nextConversation != null;
    public ConversationObject NextConversation => nextConversation;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
}
