using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVisit : MonoBehaviour
{
    public static SceneVisit ins;

    public string knotLocationName;
    public string initialKnot;

    public bool visitForest;
    public bool visitSanctuary;
    public bool visitHall;

    //find object of type
    [SerializeField] private SceneData sceneData;

    [SerializeField] private InkDialogue inkDialogue;
    [SerializeField] private CutsceneTrigger cutsceneTrigger;
    [SerializeField] private TimelineList timelineList;

    private void Awake()
    {
        if (ins != null)
        {
            //Debug.LogWarning("More than one instance of SceneVisit found!");
            return;
        }
        ins = this;

        allFalse();

        //Let GameManager call it
        //runVisit();
    }
    private void OnLevelWasLoaded(int level)
    {
        //Let GameManager call it
        //runVisit();
    }

    public void runVisit()
    {
        initialKnot = "";
        getRef();
        getKnot();
        triggerKnot();
    }


    private void allFalse()
    {
        visitForest = false;
        visitSanctuary = false;
        visitHall = false;
    }

    private void getRef()
    {
        sceneData = SceneData.ins;

        //inkDialogue = FindObjectOfType<InkDialogue>();
        inkDialogue = GameObject.Find("CanvasDialogue").GetComponent<InkDialogue>();
        cutsceneTrigger = FindObjectOfType<CutsceneTrigger>();
        timelineList = FindObjectOfType<TimelineList>();
    }

    private void getKnot()
    {
        knotLocationName = sceneData.knotLocationName;
        //initialKnot = sceneData.initialKnot;
    }


    private void triggerKnot()
    {
        switch (knotLocationName)
        {
            case "Location_Forest":
                if (visitForest == false)
                {
                    //Debug.Log("Forest First Time!");
                    visitForest = true;
                    //run cutscene
                    //Debug.Log("timeline" + timelineList.timelineObjects[0]);
                    cutsceneTrigger.GetCutscene(timelineList.timelineObjects[0]);
                    cutsceneTrigger.TriggerCutscene();
                }
                break;
            case "Location_The_Sanctuary":
                if (visitSanctuary == false)
                {
                    visitSanctuary = true;
                    initialKnot = "Cutscene_Welcome_to_Sanctuary";
                    inkDialogue.knotName = initialKnot;
                    cutsceneTrigger.GetCutscene(timelineList.timelineObjects[4]);
                    cutsceneTrigger.TriggerCutscene();
                    //Debug.Log("should give knot");
                }
                break;
            case "Location_Meeting_Hall":
                Debug.Log("SceneVisit: " + visitHall);
                if (visitHall == false)
                {
                    visitHall = true;
                    initialKnot = "Cutscene_Meeting_in_the_Meeting_Hall.At_Meeting_Hall";
                    inkDialogue.knotName = initialKnot;
                    cutsceneTrigger.GetCutscene(timelineList.timelineObjects[9]);
                    Debug.Log(inkDialogue.knotName);
                    cutsceneTrigger.TriggerCutscene();
                }
                break;
        }
    }

}
