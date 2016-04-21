using UnityEngine;
using System.Collections;

public class Inspectable : MonoBehaviour {

    public string[] InspectText;
    private InspectViewToggle inspectViewToggle;
    void Start()
    {
        inspectViewToggle = GameObject.Find("MainCamera").GetComponent<InspectViewToggle>();
        if (inspectViewToggle == null)
            Debug.LogError("No InspectViewToggle found in MainCamera.");
    }
    void OnTriggerStay()
    {
        // todo5 : use inputmanager instead of keycodes
        if(Input.GetKeyDown(KeyCode.E))
        {
            inspectViewToggle.StartInspectView();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            //show next InspectText
            //if no more text do inspectViewToggle.ExitInspectView();
        }
    }
}
