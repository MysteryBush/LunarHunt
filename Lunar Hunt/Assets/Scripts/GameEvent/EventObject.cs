using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "GameEvent/EventObject")]
public class EventObject : ScriptableObject
{
    new public string name = "New Event Object";
    public bool isUniqueObj = true;
    [SerializeField] [TextArea] private string desc;
    [SerializeField] [TextArea] public string[] descList;
    [SerializeField] public TimelineAsset triggerCutscene;

    public string Name => name;
    public string Desc => desc;
    public string[] DescList => descList;

    public void Trigger()
    {
        //TimelineAsset GiveCutscene = eventObject.triggerCutscene;
        //CutsceneTrigger.instance.TriggerCutscene(GiveCutscene);
        CutsceneTrigger.ins.GetCutscene(triggerCutscene);
        CutsceneTrigger.ins.TriggerCutscene();
        //CutsceneTrigger.instance.gameObject.SetActive(true);
    }
}
