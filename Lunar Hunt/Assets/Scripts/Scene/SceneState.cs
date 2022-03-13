using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState : MonoBehaviour
{
    public static SceneState ins;
    [SerializeField] private CutsceneTrigger cutsceneTrigger;

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
        cutsceneTrigger = CutsceneTrigger.ins;
    }
}
