using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private SpeakerObject speakerObject;
    [SerializeField] [TextArea] private string[] dialogue;

    public string Speaker => speakerObject.name;
    public Sprite Portrait => speakerObject.portrait;
    public string[] DialogueList => dialogue;
}