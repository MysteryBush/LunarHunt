using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifierQueue : MonoBehaviour
{
    #region Singleton
    public static NotifierQueue ins;

    private void Awake()
    {
        if (ins != null)
        {
            Debug.LogWarning("More than one instance of NotifierQueue found!");
            return;
        }
        ins = this;
    }

    #endregion

    public NotifierUI notifierUi;

    public delegate void OnEventChanged();
    public OnEventChanged onEventChangedCallback;

    public List<Notifier> notifierList = new List<Notifier>();

    #region Manage Notifications list
    public bool Add(Notifier notifier)
    {
        notifierList.Add(notifier);
        //Debug.Log("Added " + notifier.NotifyTitle);
        if (onEventChangedCallback != null)
            onEventChangedCallback.Invoke();
        return true;
    }

    public void Remove(Notifier notifier)
    {
        notifierList.Remove(notifier);
        //Debug.Log("Removed " + notifier.NotifyTitle);
        if (onEventChangedCallback != null)
            onEventChangedCallback.Invoke();
    }
    #endregion

    public void AddList(Notifier[] notifier)
    {
        for (int i = 0; i < notifier.Length; i++)
        {
            Add(notifier[i]);
            Debug.Log("adding queue #" + i);
        }
    }

    public void RemoveList(Notifier[] notifier)
    {
        for (int i = 0; i < notifier.Length; i++)
        {
            Remove(notifier[i]);
        }
    }

    public void NotifyAlert()
    {
        if (notifierList.Count > 0)
        {
            //Debug.Log("Run NotifyAlert -> notifierUi.runNotifications");
            notifierUi.runNotifications(this);
        }
        //Remove(notifierList[0]);
    }
    //public Notifier formNotify(string title, string obj, string[] desc)
    public Notifier formNotify(string title, string obj, string[] desc)
    {
        //Notifier newNotifier = GameObject.AddComponent<Notifier>();

        //Notifier newNotifier = new Notifier();
        //newNotifier.notificationTitle = title;
        //newNotifier.notifyObj = obj;
        //newNotifier.desc = desc;

        Notifier newNotifier = new Notifier
        {
            notificationTitle = title,
            notifyObj = obj,
            desc = desc
        };

        return newNotifier;
    }

    public void notifyEvent(EventObject eventObject)
    {
        if (eventObject.DescList.Length == 0)
        {
            return;
        }
        string[] descList = new string[eventObject.DescList.Length];
        descList = eventObject.DescList;

        Add(formNotify("Event", eventObject.name, descList));
        //NotifyAlert();
    }

    public void notifyItem(Item item)
    {
        string[] descList = new string[1];
        descList[0] = item.itemType + " collected \"" + item.name + "\"";

        Add(formNotify("Item", item.name, descList));
        //Debug.Log("formed notifyItem");
        NotifyAlert();
    }

    public void notifyFormEvidence(EvidenceRequirement currentRequirement, int value)
    {
        string[] descList = new string[1];
        string issue = "";
        switch (value)
        {
            case 0:
                issue = " because of fake news!";
                break;
            case 1:
                issue = " because lack of clues!";
                break;
        }


        descList[0] = "Can't solve " + currentRequirement.caseName + issue;

        Add(formNotify("Failed Evidence", currentRequirement.caseName, descList));
    }

    public void notifyOpenDialogue(string knotName)
    {
        string[] descList = new string[1];
        Add(formNotify("OpenDialogue", knotName, descList));
    }
}
