using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerList : MonoBehaviour
{
    #region Singleton

    public static SpeakerList instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SpeakerList found!");
            return;
        }
        instance = this;
    }

    #endregion

    public List<SpeakerObject> speakerObjects = new List<SpeakerObject>();

    public void getSpeakerObject(string name)
    {

    }
}
