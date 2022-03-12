using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVisit : MonoBehaviour
{
    public static SceneVisit ins;

    public bool visitForest;
    public bool visitSanctuary;
    public bool visitHall;

    private void Awake()
    {
        if (ins != null)
        {
            //Debug.LogWarning("More than one instance of SceneVisit found!");
            return;
        }
        ins = this;

        allFalse();
    }

    private void allFalse()
    {
        visitForest = false;
        visitSanctuary = false;
        visitHall = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        //get instant timeline to run like a level state change
    }
}
