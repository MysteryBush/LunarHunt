using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifierQueue : MonoBehaviour
{
    public delegate void OnEventChanged();
    public OnEventChanged onEventChangedCallback;

    public Notifier[] notifierList;

    public Notifier[] NotifierList => notifierList;
}
