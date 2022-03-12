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

        timelineObject.SetActive(false);

        player = FindObjectOfType<PlayerControl>();
        playableDirector = timelineObject.GetComponent<PlayableDirector>();
    }

    #endregion
    //public GameObject timelineObject;
    //private PlayableDirector playableDirector;
    //public PlayableAsset cutscene;
    //public TimelineAsset CutsceneObject;
    //public GameObject mainCamera;
    //public GameObject transitioncanvas;

    [SerializeField] private PlayerControl player;
    [SerializeField] public GameObject timelineObject;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private PlayableAsset cutscene;
    [SerializeField] private TimelineAsset CutsceneObject;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject transitioncanvas;
    private void Start()
    {
        //playableDirector = timelineObject.GetComponent<PlayableDirector>();
    }

    public void GetCutscene(TimelineAsset cutsceneObject)
    {
        CutsceneObject = cutsceneObject;
        playableDirector.playableAsset = CutsceneObject;
        cutscene = CutsceneObject;
    }
    public void TriggerCutscene()
    {
        timelineObject.SetActive(true);
        //StartCoroutine(FinishCut());
        startCutscene();
    }

    //using endCutscene via Signal instead
    //public IEnumerator FinishCut()
    //{
    //    yield return new WaitForSeconds(5);
    //    timelineObject.SetActive(false);
    //    transitioncanvas.SetActive(false);
    //    mainCamera.SetActive(true);
    //}    

    public void startCutscene()
    {
        player.isControl = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void endCutscene()
    {
        player.isControl = true;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        timelineObject.SetActive(false);
        transitioncanvas.SetActive(false);
        //mainCamera.SetActive(true);
    }
}
