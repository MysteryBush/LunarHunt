using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TimelineList : MonoBehaviour
{
    #region Singleton

    public static TimelineList instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of TimelineList found!");
            return;
        }
        instance = this;
    }

    #endregion

    public List<TimelineAsset> timelineObjects = new List<TimelineAsset>();

}
