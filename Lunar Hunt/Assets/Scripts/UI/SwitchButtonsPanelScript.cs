using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Keep button pressed after selection
//https://answers.unity.com/questions/1472975/keep-button-pressed-after-selection.html


public class SwitchButtonsPanelScript : MonoBehaviour
{

    [SerializeField]
    private Button[] buttons;

    public void SetAllButtonsInteractable()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void OnButtonClicked(Button clickedButton)
    {
        int buttonIndex = System.Array.IndexOf(buttons, clickedButton);

        if (buttonIndex == -1)
            return;

        SetAllButtonsInteractable();
        IngameMenu.ins.selectMenuTab(buttonIndex);
        clickedButton.interactable = false;
    }
}