using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/EvidenceRequirement")]
public class EvidenceRequirement : ScriptableObject
{
    public string caseName;
    public Item outputEvidence;
    [SerializeField] public Item[] clueRequirement;
}
