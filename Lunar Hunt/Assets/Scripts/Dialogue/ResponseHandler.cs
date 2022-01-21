using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;
    [SerializeField] private EventTracking eventTracking;

    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;


    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;

        for (int i = 0; i < responses.Length; i++)
        {
            if (responses[i].RequireEvent == null || eventTracking.eventObjects.Contains(responses[i].RequireEvent))
            {
                Response response = responses[i];
                int responseIndex = i;

                GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
                responseButton.gameObject.SetActive(true);
                responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
                responseButton.GetComponent<Button>().onClick.AddListener(call: () => OnPickedResponse(response, responseIndex));

                tempResponseButtons.Add(responseButton);

                responseBoxHeight += responseButtonTemplate.sizeDelta.y;
            }
            else
            {
                continue;
            }
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, y: responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);

        //destroy choices button
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        //if there is response, do something? or what is this?
        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }
        responseEvents = null;

        //If response has ConversationObject, use it
        if (response.ConversationObject)
        {
            dialogueUI.runConversation(response.ConversationObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }

        //dialogueUI.runConversation(response.ConversationObject);
    }

    public bool checkResponsesOption(Response[] responses)
    {
        int disabledResponse = 0;
        for (int i = 0; i < responses.Length; i++)
        {
            if (responses[i].RequireEvent != null && !eventTracking.eventObjects.Contains(responses[i].RequireEvent))
            {
                disabledResponse += 1;
            }
        }
        if (disabledResponse >= responses.Length)
        {
            Debug.Log("no responses to choose!");
            return false;
        }
        else
        {
            Debug.Log("has responses to choose!");
            return true;
        }
    }
}
