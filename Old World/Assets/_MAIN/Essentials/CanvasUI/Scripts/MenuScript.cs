using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MenuScript : MonoBehaviour 
{
    private GameObject journal;
    private Image menuBackground;
    private GameObject buttons;
    [Header("DisablesInMenu")] // only put always active scripts here
    public GameObject[] objects;
    private bool[] wasActive;

    // hardcoded script to disable in menu
    private CameraOrbit cameraOrbit; 
    private FirstPersonViewToggle cameraViewToggle;
    private BlurOptimized blur;
    private DepthOfField dofBlur;
    //private CameraLookAt mouseToggle;

    private HowToPlayScript howToPlay;

    private bool pause;
    private bool oldPause;
    private bool restart;
    private bool oldRestart;
    //private bool openJournal;
    private bool oldOpenJournal;
    private bool openPlayerInfo;
    private bool oldOpenPlayerInfo;

	FMOD.Studio.EventInstance snapshotPause;
	FMOD.Studio.EventInstance snapshotUnpause;


   // private ItemParent currentCompareItem;
    void Awake()
    {
		snapshotPause = FMODUnity.RuntimeManager.CreateInstance ("snapshot:/Paus");

		howToPlay = GetComponentInChildren<HowToPlayScript>();
        wasActive = new bool[objects.Length];
        journal = gameObject.FindChildObject("Journal");
        if (journal == null) Debug.LogError("The journal is missing!");
        buttons = gameObject.FindChildObject("MenuButtons");
        if (buttons == null) Debug.LogError("The buttons are missing!");
        buttons.SetActive(true);
        menuBackground = GetComponent<Image>();
        journal.SetActive(true); //minns inte exakt varför jag sätter dem till active här, tror det var någon initieringsbug
        menuBackground.enabled = true;

        GameObject camera = GameObject.Find("MainCamera");
        cameraOrbit = camera.GetComponent<CameraOrbit>();
        blur = camera.GetComponent<BlurOptimized>();
        dofBlur = camera.GetComponent<DepthOfField>();

        cameraViewToggle = camera.GetComponent<FirstPersonViewToggle>();

        menuBackground.enabled = false;
        journal.SetActive(false);
        buttons.SetActive(false);

        PauseGame();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Menu") && StateController.menuOpen)
        {
            ResumeGame();
        }
        else if (Input.GetButtonDown("Menu") && StateController.menuOpen == false)
        {
            PauseGame();
        }
        else if (Input.GetKeyUp(KeyCode.F5))
        {
            cameraOrbit.enabled = false;
            StateController.cursorLocked = false;
        }
        else if (Input.GetKeyUp(KeyCode.F6))
        {
            cameraOrbit.enabled = true;
            StateController.cursorLocked = true;
        }
        if (StateController.cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!StateController.cursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ResumeGame()
    {
		snapshotPause.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
        dofBlur.enabled = false;
        blur.enabled = false;

        //DisableJournal();
        //journal.SetActive(false);
        //menuBackground.enabled = false;
        StateController.menuOpen = false;
        buttons.SetActive(false);
        StateController.cursorLocked = true;
        cameraOrbit.enabled = oldCameraOrbit;
        Time.timeScale = 1.0f;
        howToPlay.Deactivate();
    }
    private bool oldCameraOrbit = false;
    public void PauseGame()
    {
		snapshotPause.start ();
        dofBlur.enabled = true;
        blur.enabled = true;

        StateController.menuOpen = true;
        //EnableJournal();
        //journal.SetActive(true);
        //menuBackground.enabled = true;
        buttons.SetActive(true);
        StateController.cursorLocked = false;
        oldCameraOrbit = cameraOrbit.enabled;
        cameraOrbit.enabled = false;
        Time.timeScale = 0.0f;
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR  
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void DisableJournal()
    {
        //openJournal = true;
        cameraOrbit.enabled = false;
        cameraViewToggle.enabled = true;
        for (int i = 0; i < objects.Length; i++)
        {
            wasActive[i] = objects[i].activeSelf;
            objects[i].SetActive(false);
        }
    }

    private void EnableJournal()
    {
        //openJournal = false;
        cameraOrbit.enabled = true;
        cameraViewToggle.enabled = false;
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(wasActive[i]);
        }
    }

}