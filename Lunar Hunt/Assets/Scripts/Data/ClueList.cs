using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueList : MonoBehaviour
{
    #region Singleton

    public static ClueList instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ClueList found!");
            return;
        }
        instance = this;
    }

    #endregion

    public List<Item> clueItems = new List<Item>();

    public void getClueItem(string name)
    {

    }
}
