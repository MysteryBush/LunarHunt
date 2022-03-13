using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CutsceneTrigger: MonoBehaviour
{
    #region Singleton
    public static CutsceneTrigger ins;

    private void Awake()
    {
        if (ins != null)
        {
            Debug.LogWarning("More than one instance of CutsceneTrigger found!");
            return;
        }
        ins = this;

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

    public bool timelineBlock;
    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        playableDirector = timelineObject.GetComponent<PlayableDirector>();
    }

    public void GetCutscene(TimelineAsset cutsceneObject)
    {
        CutsceneObject = cutsceneObject;
        cutscene = CutsceneObject;
        //playableDirector.playableAsset = CutsceneObject;
        //playableDirector.playableAsset = cutscene;
        timelineObject.GetComponent<PlayableDirector>().playableAsset = cutscene;
        Debug.Log("Playing cutscene: " + cutscene);
    }
    public void TriggerCutscene()
    {
        //timelineObject.SetActive(true);
        //Debug.Log(timelineObject);
        //Debug.Log("SetActive True to timelineObject");
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
        player = FindObjectOfType<PlayerControl>();
        player.isControl = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        //dialogue can't continue during timeline until other function say so
        timelineBlock = true;

        //trigger Play On Awake
        timelineObject.SetActive(false);
        timelineObject.SetActive(true);

        //Debug.Log("timelineBlock: " + timelineBlock);

    }

    public void endCutscene()
    {
        //Debug.Log("Ending cutscene");
        player.isControl = true;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        timelineObject.SetActive(false);
        transitioncanvas.SetActive(false);
        //mainCamera.SetActive(true);
        timelineBlock = false;
    }

    public void dialogueCutscene()
    {
        timelineBlock = false;
        FindObjectOfType<InkDialogue>().OpenDialogueBox();
    }
}
