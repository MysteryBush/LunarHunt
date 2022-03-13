using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCase : MonoBehaviour
{
    public int currentEvidenceRequirement;
    //current case clues
    [SerializeField] public List<Item> currentCaseClues = new List<Item>();
}
