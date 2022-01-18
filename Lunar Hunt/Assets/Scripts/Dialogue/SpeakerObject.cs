using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/SpeakerObject")]
public class SpeakerObject : ScriptableObject
{
    new public string name = "New Speaker";
    public Sprite portrait = null;
    //public bool isDefaultSpeaker = false;
    public virtual void Speak()
    {
        Debug.Log(name + "is speaking");
    }
}
