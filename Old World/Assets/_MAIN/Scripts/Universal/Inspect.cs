using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Inspect : MonoBehaviour
{
    private static GameObject inspectBox;
    private static Text boxHeadline;
    private static Text boxText;
    private static float characterPerSeconds = 50f;
    public string inspectHeadline = "";
    public List<TextAsset> inspectText = new List<TextAsset>();
    private string inspectString = "";
    //private char[] charArr;
    private int currentTextID = 0;
    private InspectViewToggle inspectViewToggle;
    private bool canBeInspected = false;

    private int charIndex = -1;
    private int oldCharIndex = -1;
    private float charIndexFloat = 0;
    private int stringLength = 0;


    void Awake()
    {
     
    }
    private Renderer rend;
    private Text inspectPrompt;
    void Start()
    {
        inspectPrompt = GameObject.Find("InspectPrompt").GetComponent<Text>();
        inspectPrompt.enabled = false;
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_EmissionColor", Color.black);

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

    [Range(0, 1)]
    public float red = 1;
    [Range(0, 1)]
    public float green = 1;
    [Range(0, 1)]
    public float blue = 1;

    void Update()
    {
        rend.material.SetColor("_EmissionColor", new Color(red, green, blue, 1));
        if (canBeInspected)
        {
            if (Input.GetButtonDown("Action") && !StateController.currentView.Equals(CameraStatus.InspectView))
            {
                disableInspectPrompt();
                currentTextID = 0;
                charIndex = 0;
                oldCharIndex = 0;
                charIndexFloat = 0;
                inspectString = inspectText[currentTextID].text;
                //charArr = inspectString.ToCharArray();
                stringLength = inspectString.Length;
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
                    oldCharIndex = stringLength;
                    boxText.text = inspectString.Substring(0, charIndex);
                }
                //new page
                else
                {
                    currentTextID++;
                    // pages left
                    if (currentTextID < inspectText.Count)
                    {
                       charIndex = 0;
                        oldCharIndex = 0;
                        charIndexFloat = 0;
                        inspectString = inspectText[currentTextID].text;
                        //charArr = inspectString.ToCharArray();
                        stringLength = inspectString.Length;
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

        //update text
        if (charIndex != -1 && charIndex < stringLength)
        {
            charIndexFloat += Time.deltaTime * characterPerSeconds;
            oldCharIndex = charIndex;
            charIndex = (int)charIndexFloat;
            if (stringLength < charIndex)
                charIndex = stringLength;

            /*   if(charIndex != oldCharIndex)
               {
                   soundCharacterPrint.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
                   soundCharacterPrint.start();
               }*/

            /*     if(inspectString.)
                 // kolla efter style <*>
                 /*
                 if(style)
                 {   
                     öka charIndex 3 steg första gången

                     kolla efter </*>
                     if(endstyle)
                     {
                         öka charindex 3 steg
                     }
                     else
                       adda +</*> i slutet
                 }
                 */
            boxText.text = inspectString.Substring(0, charIndex);
        }
    }

    void OnTriggerStay()
    {
        if (StateController.hasGottenInspectedPromptPaper == false)
        {
            activateInspectPrompt();
            StateController.hasGottenInspectedPromptPaper = true;
        }
        canBeInspected = true;
    }

    void OnTriggerExit()
    {
        disableInspectPrompt();
        canBeInspected = false;
    }

    private void activateInspectPrompt()
    {
        inspectPrompt.enabled = true;
    }

    private void disableInspectPrompt()
    {
        inspectPrompt.CrossFadeAlpha(0, 2, true);
    }
}
