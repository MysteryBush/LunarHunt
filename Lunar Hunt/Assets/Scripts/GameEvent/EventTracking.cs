using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Might have to learn about "Singleton Patterns"

public class EventTracking : MonoBehaviour
{
    #region Singleton
    public static EventTracking instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EventTracking found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnEventChanged();
    public OnEventChanged onEventChangedCallback;

    public List<EventObject> eventObjects = new List<EventObject>();

    //public QuestTracking quest;


    #region Manage item
    public bool Add(EventObject eventObject)
    {
        //If event is unique, there can be only one added
        if (eventObject.isUniqueObj)
        {
            eventObjects.Add(eventObject);
            Debug.Log("Added " + eventObject.name);
            if (onEventChangedCallback != null)
                onEventChangedCallback.Invoke();
        }
        else
        {
            eventObjects.Add(eventObject);
            Debug.Log("Added " + eventObject.name);
            if (onEventChangedCallback != null)
                onEventChangedCallback.Invoke();
        }

        return true;
    }
    public void Remove(EventObject eventObject)
    {
        eventObjects.Remove(eventObject);
        Debug.Log("Removed " + eventObject.name);
        if (onEventChangedCallback != null)
            onEventChangedCallback.Invoke();
    }
    #endregion

    public void AddEvent(EventObject[] eventObjects)
    {
        for (int i = 0; i < eventObjects.Length; i++)
        {
            Add(eventObjects[i]);
            Debug.Log("adding item #" + i);
        }
    }

    public void RemoveEvent(EventObject[] eventObjects)
    {
        for (int i = 0; i < eventObjects.Length; i++)
        {
            Remove(eventObjects[i]);
        }
    }
}

