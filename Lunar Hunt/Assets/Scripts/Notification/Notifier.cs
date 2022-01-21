using UnityEngine;

[System.Serializable]
public class Notifier
{
    [SerializeField] private string notificationTitle;
    [SerializeField] [TextArea] private string[] desc;

    public string NotifyTitle => notificationTitle;
    public string[] NotifyDesc => desc;
}