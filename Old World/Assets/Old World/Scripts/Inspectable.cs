using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Inspectable : MonoBehaviour
{
    private static GameObject inspectBox;
    private static Text boxHeadline;
    private static Text boxText;
    private static float characterPerSeconds = 50f;
    public string inspectHeadline = "";
    public List<TextAsset> inspectText = new List<TextAsset>();
    private int currentTextID = 0;
    private InspectViewToggle inspectViewToggle;
    private bool canBeInspected = false;

    private int charIndex = -1;
    private float charIndexFloat = 0;
    private int stringLength = 0;

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
                charIndex = 0;
                charIndexFloat = 0;
                stringLength = inspectText[currentTextID].text.Length;
                inspectBox.SetActive(true);
                inspectViewToggle.StartInspectView(transform.position);
                boxHeadline.text = inspectHeadline;
                //boxText.text = inspectText[currentTextID].text;
            }
            else if (Input.GetButtonDown("InspectSkip"))
            {
                //characters left
                if (charIndex < stringLength)
                {
                    charIndex = stringLength;
                    boxText.text = inspectText[currentTextID].text.Substring(0, charIndex);
                }
                //new page
                else
                {
                    currentTextID++;
                    // pages left
                    if (currentTextID < inspectText.Count)
                    {
                        charIndex = 0;
                        charIndexFloat = 0;
                        stringLength = inspectText[currentTextID].text.Length;
                    }
                    // no pages left 
                    else
                    {
                        inspectViewToggle.ExitInspectView();
                        inspectBox.SetActive(false);
                    }
                }
            }
        }

        if(charIndex != -1 && charIndex < stringLength)
        {
            charIndexFloat += Time.deltaTime * characterPerSeconds;
            charIndex = (int)charIndexFloat;
            if (stringLength < charIndex)
                charIndex = stringLength;
            boxText.text = inspectText[currentTextID].text.Substring(0, charIndex);
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
