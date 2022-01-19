using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private string dialogueText;
    [SerializeField] [TextArea] private string[] dialogue;

    public string DialogueText => dialogueText;
    public string[] DialogueList => dialogue;
}