using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Inspectable : MonoBehaviour
{
    private static GameObject inspectBox;
    private static Text boxHeadline;
    private static Text boxText;

    public string inspectHeadline = "";
    public List<TextAsset> inspectText = new List<TextAsset>();
    private int currentTextID = 0;
    private InspectViewToggle inspectViewToggle;
    private bool canBeInspected = false;
    void Start()
    {
        if (inspectHeadline.Equals(""))
            Debug.LogWarning("Here is where I would put my headline, IF I HAD ANY!");
        if (inspectText.Count < 1)
            Debug.LogWarning("Here is where I would put my text, IF I HAD ANY!");
       

        if (inspectBox == null)
        {
            inspectBox = GameObject.Find("_CanvasUI").FindChildObject("InspectBox");
            if (inspectBox == null)
                Debug.LogError("No InspectBox found");
            else
            {
                boxHeadline = inspectBox.FindChildObject("InspectHeadline").GetComponent<Text>();
                boxText = inspectBox.FindChildObject("InspectText").GetComponent<Text>();
            }
        }
        inspectViewToggle = GameObject.Find("MainCamera").GetComponent<InspectViewToggle>();
        if (inspectViewToggle == null)
            Debug.LogError("No InspectViewToggle found in MainCamera.");
    }

    void Update()
    {
        //todo add character by character instead of string.
        if (canBeInspected)
        {
            if (Input.GetButtonDown("Inspect"))
            {
                currentTextID = 0;
                inspectBox.SetActive(true);
                inspectViewToggle.StartInspectView(transform.position);
                boxHeadline.text = inspectHeadline;
                boxText.text = inspectText[currentTextID].text;
            }
            else if (Input.GetButtonDown("InspectSkip"))
            {

                //todo if more text, print all text directly
                // else
                currentTextID++;
                if (currentTextID < inspectText.Count)
                {
                    boxText.text = inspectText[currentTextID].text;
                }
                else
                {
                    inspectViewToggle.ExitInspectView();
                    inspectBox.SetActive(false);
                }
            }
        }
    }

    void SetTextBoxText(string page)
    {

    }

    void OnTriggerStay()
    {
        /*if(todo2 isInVisionOfPlayer) // vision of player can also check if player is close enough in case it was something else that triggered
        {*/
        canBeInspected = true;
        //}
    }

    void OnTriggerExit()
    {
        canBeInspected = false;
    }
}
