using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

    public static IntroScript instance;

    public List<TextAsset> inspectText = new List<TextAsset>();

    public List<GameObject> backgrounds = new List<GameObject>();

    //public List<Texture> pictures = new List<Texture>();
    //public Texture dark;

    private int picIndex;
    public int currentTextFileID;
    public int finalImageSteps = 0;

    private string text;
    private string printedText;
    private int textIndex;
    private float textSpeed = 0.03f;
    private bool textDone = false;

    private float alpha = 1;
    private Color screenColor;
    public GUISkin introGUI;
    public bool textBoxShown = true;


    [FMODUnity.EventRef]
    public static FMOD.Studio.EventInstance musicEvent;
    public static FMOD.Studio.ParameterInstance firstFade;
    public static FMOD.Studio.ParameterInstance skip;
    public static FMOD.Studio.ParameterInstance progress;

    [FMODUnity.EventRef]
    public static FMOD.Studio.EventInstance voiceEvent;
    public static FMOD.Studio.ParameterInstance voice;
    //  public static string skip = "Skip";
    //   public static string progress = "Progress";

    //public GUITexture GUITEXTURE;
    public float fadeSpeed;

    void Awake()
    {
        instance = this;
        musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Intro/IntroMusic");
        musicEvent.getParameter("FirstFade", out firstFade);
        musicEvent.getParameter("Skip", out skip);
        musicEvent.getParameter("Progress", out progress);

        voiceEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Intro/Intro");
        voiceEvent.getParameter("Voice", out voice);
     
        voice.setValue(1);
        voiceEvent.start();
        firstFade.setValue(0);
        skip.setValue(0);
        progress.setValue(0);
        musicEvent.start();
    }

    // Use this for initialization
    void Start () {
        currentTextFileID = 0;
        InvokeRepeating("printer", 0, textSpeed);
        currentTextFileID = 0;
        picIndex = 0;

        text = inspectText[currentTextFileID].text;

        //GUIFade(0.5f, true);

        Camera.main.GetComponent<movement>().cameraMovement();
        Camera.main.GetComponent<movement>().speed = 0.3f;

    }

    void printer()
    {
        if (textIndex < text.Length)
        {
            textDone = false;
            printedText = printedText + text[textIndex];
            textIndex++;
            
        } else {
            textDone = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //GUITEXTURE.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        if (Input.GetButtonDown("Action")) 
        {
            writer();
        }
    }
    void writer()
    {

        if (currentTextFileID == 17 && textDone == true)
        {

            textBoxShown = false;
            //Change to where hub is!!!!!!! --------------------------------------------------------------------------< IMPORTANT
            
            return;
            //Application.LoadLevel(1);

        }
        /*if (inspectText.Count < currentTextFileID )
        {
            return;
        }*/

        if (textDone == true)
        {
            textIndex = 0;
            currentTextFileID++;
            printedText = "";
            text = inspectText[currentTextFileID].text;
        }
        else
        {
            textIndex = text.Length;
            printedText = text;
            return;
        }
        if (currentTextFileID == 0)
        {
            //Line 1
            //denna körs inte - Tobias
        }
        if (currentTextFileID == 1)
        {
            //Line 2
            voice.setValue(2);
            voiceEvent.start();
        }
        if (currentTextFileID == 2)
        {
            //Line 3
            Camera.main.GetComponent<movement>().cameraMovement();
            Camera.main.GetComponent<movement>().speed = 0.8f;
            voice.setValue(4);
            voiceEvent.start();
        }
        if (currentTextFileID == 3)
        {

            firstFade.setValue(1);
            Camera.main.GetComponent<movement>().cameraMovement();
            Camera.main.transform.position = Camera.main.GetComponent<movement>().positions[Camera.main.GetComponent<movement>().posIndex];
            voice.setValue(5);
            voiceEvent.start();
        }
        if (currentTextFileID == 4)
        {
            progress.setValue(1);
            Camera.main.GetComponent<movement>().speed = 0.1f;
            Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(6);
            voiceEvent.start();

        }
        if (currentTextFileID == 5)
        {

            //Camera.main.GetComponent<movement>().cameraMovement();
            //Camera.main.transform.position = Camera.main.GetComponent<movement>().positions[Camera.main.GetComponent<movement>().posIndex];
            voice.setValue(7);
            voiceEvent.start();

        }
        if (currentTextFileID == 6)
        {

            Camera.main.GetComponent<movement>().speed = 0.3f;
            Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(8);
            voiceEvent.start();

        }
        if(currentTextFileID == 7)
        {
            voice.setValue(9);
            voiceEvent.start();
        }
        if (currentTextFileID == 8)
        {
            
            progress.setValue(2);
            Camera.main.GetComponent<movement>().speed = 0.3f;
            Camera.main.GetComponent<movement>().cameraMovement();
            //Camera.main.transform.position = Camera.main.GetComponent<movement>().positions[Camera.main.GetComponent<movement>().posIndex];
            //Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(11);
            voiceEvent.start();
        }
        if (currentTextFileID == 9)
        {

            Camera.main.GetComponent<movement>().speed = 0.3f;
            Camera.main.GetComponent<movement>().cameraMovement();
            //Camera.main.transform.position = Camera.main.GetComponent<movement>().positions[Camera.main.GetComponent<movement>().posIndex];
            //Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(12);
            voiceEvent.start();
        }
        if (currentTextFileID == 10)
        {
            progress.setValue(3);

            Camera.main.GetComponent<movement>().speed = 0.3f;
            Camera.main.GetComponent<movement>().cameraMovement();

            //Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(13);
            voiceEvent.start();
        }
        if (currentTextFileID == 11)
        {
            
            Camera.main.GetComponent<movement>().speed = 0.3f;
            Camera.main.GetComponent<movement>().cameraMovement();
            Camera.main.transform.position = Camera.main.GetComponent<movement>().positions[Camera.main.GetComponent<movement>().posIndex];
            Camera.main.GetComponent<movement>().cameraMovement();
            //Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(14);
            voiceEvent.start();
        }
        if (currentTextFileID == 12)
        {

            Camera.main.GetComponent<movement>().speed = 0.3f;
            Camera.main.GetComponent<movement>().cameraMovement();
            Camera.main.transform.position = Camera.main.GetComponent<movement>().positions[Camera.main.GetComponent<movement>().posIndex];
            Camera.main.GetComponent<movement>().cameraMovement();
            //Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(15);
            voiceEvent.start();
        }
        if (currentTextFileID == 13)
        {
            
            progress.setValue(4);
            Camera.main.GetComponent<movement>().speed = 0.3f;
            Camera.main.GetComponent<movement>().cameraMovement();
            Camera.main.transform.position = Camera.main.GetComponent<movement>().positions[Camera.main.GetComponent<movement>().posIndex];
            Camera.main.GetComponent<movement>().speed = 0.15f;
            Camera.main.GetComponent<movement>().cameraMovement();
            //Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(16);
            voiceEvent.start();
        }
        if (currentTextFileID == 14)
        {
            voice.setValue(17);
            voiceEvent.start();
        }
        if (currentTextFileID == 15)
        {
            
            progress.setValue(5);
            Camera.main.GetComponent<movement>().speed = 0.5f;
            Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(18);
            voiceEvent.start();
        }
        if (currentTextFileID == 16)
        {
            Camera.main.GetComponent<movement>().speed = 0.1f;
            Camera.main.GetComponent<movement>().cameraMovement();
            voice.setValue(19);
            voiceEvent.start();
            progress.setValue(6);
            finalImage(3.0f);
        }
        if (currentTextFileID == 17)
        {
            voice.setValue(20);
            voiceEvent.start();
        }

    }
    public void finalImage(float timer)
    {
        if (finalImageSteps >= 4)
        {
            return;
        }
        StartCoroutine(delay(timer));
    }
    IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);
        finalImageSteps++;
        if (finalImageSteps > 3)
        {
            finalImage(2.5f);
        }
        else
        {
            finalImage(1.5f);
        }
    }

    void OnGUI()
    {
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), dark);

        //GUI window
        GUI.skin = introGUI;

        GUI.skin.label.fontSize = Mathf.RoundToInt(18 * Screen.width / (1080 * 1.0f));

        //screenColor = new Color(alpha, alpha, alpha, alpha);

        //GUI.color = screenColor;
        //GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        if (textBoxShown == true)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 6, Screen.height / 1.5f, Screen.width / 1.5f, Screen.height / 5));
            //GUILayout.BeginArea(new Rect(100, 100, 50, 1000 ));

            GUILayout.BeginVertical("", GUI.skin.GetStyle("window"));

            GUILayout.Label(printedText);

            GUILayout.EndVertical();

            GUILayout.EndArea();
        }

    }
}
