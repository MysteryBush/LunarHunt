using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Notifier
{
    [SerializeField] public string notificationTitle;
    [SerializeField] public string notifyObj;
    [SerializeField] [TextArea] public string[] desc;

    public string NotifyTitle => notificationTitle;
    public string NotifyObj => notifyObj;
    public string[] NotifyDesc => desc;
}