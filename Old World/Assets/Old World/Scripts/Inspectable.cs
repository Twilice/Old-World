using UnityEngine;
using System.Collections;

public class Inspectable : MonoBehaviour
{

    public string[] InspectText;
    private InspectViewToggle inspectViewToggle;
    private bool canBeInspected = false;
    private static GameObject inspectBox;
    void Start()
    {
        if (inspectBox == null)
        {
            inspectBox = GameObject.Find("_CanvasUI").FindChildObject("InspectBox");
            if (inspectBox == null)
                Debug.LogError("No InspectBox found");
        }
        inspectViewToggle = GameObject.Find("MainCamera").GetComponent<InspectViewToggle>();
        if (inspectViewToggle == null)
            Debug.LogError("No InspectViewToggle found in MainCamera.");
    }

    void Update()
    {
        if (canBeInspected)
        {
            if (Input.GetButtonDown("Inspect"))
            {
                inspectViewToggle.StartInspectView(transform.position);
                // start inspectbox with text
            }
            else if (Input.GetButtonDown("InspectSkip"))
            {
                inspectViewToggle.ExitInspectView();
                //todo5 if more text, print all text directly
                //else if no more text, load next page
                //else if no more pages, do ExitInspectView
            }
        }
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
