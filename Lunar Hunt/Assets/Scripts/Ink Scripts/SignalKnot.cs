using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalKnot : MonoBehaviour
{
    [SerializeField] private InkDialogue inkDialogue;
    [SerializeField] public string knotName;

    // Start is called before the first frame update
    void Start()
    {
        inkDialogue = GameObject.Find("CanvasDialogue").GetComponent<InkDialogue>();
    }

    public void setKnot()
    {
        inkDialogue.knotName = knotName;
    }
}
