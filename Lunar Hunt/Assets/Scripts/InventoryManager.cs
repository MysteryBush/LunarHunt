using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public static int potionCount = 0;
    public Text potionText;
    // Start is called before the first frame update
    void Start()
    {
        //potionText = GetComponent<Text>();
        potionText.text = potionCount.ToString() + "x";
    }

    // Update is called once per frame
    void Update()
    {
        potionText.text = potionCount.ToString() + "x";
    }
}
