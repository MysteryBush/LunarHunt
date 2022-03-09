using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CutsceneTrigger: MonoBehaviour
{
    #region Singleton
    public static CutsceneTrigger instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CutsceneTrigger found!");
            return;
        }
        instance = this;
    }

    #endregion
    public GameObject timelineObject;
    private PlayableDirector playableDirector;
    public PlayableAsset cutscene;
    public TimelineAsset CutsceneObject;
    public GameObject mainCamera;
    public GameObject transitioncanvas;
    private void Start()
    {
        playableDirector = timelineObject.GetComponent<PlayableDirector>();
    }

    public void GetCutscene(TimelineAsset cutsceneObject)
    {
        CutsceneObject = cutsceneObject;
        playableDirector.playableAsset = CutsceneObject;
    }
    public void TriggerCutscene()
    {
        timelineObject.SetActive(true);
        StartCoroutine(FinishCut());
    }

    public IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(5);
        timelineObject.SetActive(false);
        transitioncanvas.SetActive(false);
        mainCamera.SetActive(true);
    }    
}
