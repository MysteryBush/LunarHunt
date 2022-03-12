using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData ins;
    public string knotLocationName;
    public string initialKnot;
    private void Awake()
    {
        ins = this;
        initialKnot = "";
        switch (knotLocationName)
        {
            case "Location_Forest":
                if (SceneVisit.ins.visitForest == false)
                {
                    SceneVisit.ins.visitForest = true;
                    //Debug.Log("first visit forest!");
                    //run cutscene
                    CutsceneTrigger.instance.GetCutscene(TimelineList.instance.timelineObjects[0]);
                    CutsceneTrigger.instance.TriggerCutscene();
                }
                //InkManager.ins.runInk();
                break;
            case "Location_The_Sanctuary":
                if (SceneVisit.ins.visitSanctuary == false)
                {
                    SceneVisit.ins.visitSanctuary = true;
                    initialKnot = "Cutscene_Welcome_to_Sanctuary";
                    CutsceneTrigger.instance.GetCutscene(TimelineList.instance.timelineObjects[4]);
                    CutsceneTrigger.instance.TriggerCutscene();
                    //Debug.Log("should give knot");
                }
                //InkManager.ins.runInk();
                break;
            case "Location_Meeting_Hall":
                Debug.Log("SceneVisit: " + SceneVisit.ins.visitHall);
                if (SceneVisit.ins.visitHall == false)
                {
                    SceneVisit.ins.visitHall = true;
                    initialKnot = "Cutscene_Meeting_in_the_Meeting_Hall.At_Meeting_Hall";
                    CutsceneTrigger.instance.GetCutscene(TimelineList.instance.timelineObjects[9]);
                    CutsceneTrigger.instance.TriggerCutscene();
                }
                //InkManager.ins.runInk();
                break;
        }
    }
}
