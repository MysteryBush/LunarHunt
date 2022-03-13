using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TimelineState : MonoBehaviour
{
    [SerializeField] private SceneState sceneState;
    [SerializeField] private TimelineAsset timeline;
    [SerializeField] private int setNumber;

    // Start is called before the first frame update
    void Start()
    {
        sceneState = GameObject.Find("GameManager").GetComponent<SceneState>();
    }

    public void setTimeline()
    {
        switch (setNumber)
        {
            case 0:
                sceneState.timelineForest = timeline;
                break;
            case 1:
                sceneState.timelineSanctuary = timeline;
                break;
            case 2:
                sceneState.timelineHall = timeline;
                break;
        }
    }
}
