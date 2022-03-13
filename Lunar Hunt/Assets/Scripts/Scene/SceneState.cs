using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class SceneState : MonoBehaviour
{
    public static SceneState ins;
    [SerializeField] private CutsceneTrigger cutsceneTrigger;
    [SerializeField] private SceneVisit sceneVisit;

    public TimelineAsset setTimeline;
    public TimelineAsset timelineForest;
    public TimelineAsset timelineSanctuary;
    public TimelineAsset timelineHall;
    //public PlayableAsset timelineNursery;

    private void Awake()
    {
        if (ins != null)
        {
            //Debug.LogWarning("More than one instance of SceneState found!");
            return;
        }

        ins = this;
    }

    public void runSceneState()
    {
        cutsceneTrigger = FindObjectOfType<CutsceneTrigger>();
        sceneVisit = SceneVisit.ins;

        setSceneState();
    }

    private void setSceneState()
    {
        if (sceneVisit.firstVisit == false)
        {
            switch (sceneVisit.knotLocationName)
            {
                case "Location_Forest":
                    setTimeline = timelineForest;
                    break;
                case "Location_The_Sanctuary":
                    setTimeline = timelineSanctuary;
                    break;
                case "Location_Meeting_Hall":
                    setTimeline = timelineHall;
                    break;
            }
            if (setTimeline != null)
            {
                cutsceneTrigger.GetCutscene(setTimeline);
                cutsceneTrigger.TriggerCutscene();
                Debug.Log("setSceneState()");
            }
        }
    }
}
