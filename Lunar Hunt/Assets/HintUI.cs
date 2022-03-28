using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    #region Singleton

    public static HintUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of HintUI found!");
            return;
        }
        instance = this;
    }

    #endregion

    [SerializeField] private TMP_Text hintText;
    public string initialHint;

    // Start is called before the first frame update
    void Start()
    {
        // if (initialHint != "")
        //     hintText.text = initialHint;
    }

    public void changeHint(string hint)
    {
        hintText.text = hint;
    }
}
