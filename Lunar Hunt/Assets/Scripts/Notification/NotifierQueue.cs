using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifierQueue : MonoBehaviour
{
    #region Singleton
    public static NotifierQueue instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of NotifierQueue found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnEventChanged();
    public OnEventChanged onEventChangedCallback;

    public List<Notifier> notifierList = new List<Notifier>();

    #region Manage Notifications list
    public bool Add(Notifier notifier)
    {
        notifierList.Add(notifier);
        Debug.Log("Added " + notifier.NotifyTitle);
        if (onEventChangedCallback != null)
            onEventChangedCallback.Invoke();
        return true;
    }

    public void Remove(Notifier notifier)
    {
        notifierList.Remove(notifier);
        Debug.Log("Removed " + notifier.NotifyTitle);
        if (onEventChangedCallback != null)
            onEventChangedCallback.Invoke();
    }
    #endregion

    public void AddList(Notifier[] notifier)
    {
        for (int i = 0; i < notifier.Length; i++)
        {
            Add(notifier[i]);
            Debug.Log("adding item #" + i);
        }
    }

    public void RemoveList(Notifier[] notifier)
    {
        for (int i = 0; i < notifier.Length; i++)
        {
            Remove(notifier[i]);
        }
    }
    public Notifier formNotify(string title, string obj, string desc)
    {
        Notifier newNotifier = new Notifier();
        newNotifier.notificationTitle = title;
        newNotifier.notifyObj = obj;
        newNotifier.desc = desc;
        return newNotifier;
    }

    public void notifyEvent(EventObject eventObject)
    {
        string descList = "You witnessed " + eventObject.name;

        Add(formNotify("Event", eventObject.name, descList));
    }

    public void notifyItem(Item item)
    {
        string descList = "You collected " + item.name;

        Add(formNotify("Item", item.name, descList));
    }
}
