using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogues
{
    public string name;
    public Sprite portrait;
    public int reward;

    [TextArea(5, 10)]
    public string[] sentences;

    //public int[] reward;
}
