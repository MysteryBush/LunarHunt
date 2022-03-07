using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private SpeakerObject speakerObject;
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private DialogueObject nextDialogue;
    [SerializeField] private Response[] responses;

    public string Speaker => speakerObject.name;
    public Sprite Portrait => speakerObject.portrait;
    public string[] Dialogue => dialogue;
    public bool HasNextDialogue => nextDialogue != null;
    public DialogueObject NextDialogue => nextDialogue;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
}
