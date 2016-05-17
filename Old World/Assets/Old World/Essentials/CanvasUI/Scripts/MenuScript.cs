using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    //private CameraLookAt mouseToggle;

    private bool pause;
    private bool oldPause;
    private bool restart;
    private bool oldRestart;
    //private bool openJournal;
    private bool oldOpenJournal;
    private bool openPlayerInfo;
    private bool oldOpenPlayerInfo;

   // private ItemParent currentCompareItem;
    void Awake()
    {
        wasActive = new bool[objects.Length];
        journal = gameObject.FindChildObject("Journal");
        if (journal == null) Debug.LogError("The journal is missing!");
        buttons = gameObject.FindChildObject("MenuButtons");
        if (buttons == null) Debug.LogError("The buttons are missing!");
        buttons.SetActive(true);
        menuBackground = GetComponent<Image>();
        journal.SetActive(true); //minns inte exakt varför jag sätter dem till active här, tror det var någon initieringsbug
        menuBackground.enabled = true;
    }
	void Start() 
    {
        GameObject camera = GameObject.Find("MainCamera");
        cameraOrbit = camera.GetComponent<CameraOrbit>();
        cameraViewToggle = camera.GetComponent<FirstPersonViewToggle>();

        menuBackground.enabled = false;
        journal.SetActive(false);
        buttons.SetActive(false);
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

        //DisableJournal();
        //journal.SetActive(false);
        menuBackground.enabled = false;
        StateController.menuOpen = false;
        buttons.SetActive(false);
        StateController.cursorLocked = true;
        cameraOrbit.enabled = true;
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        StateController.menuOpen = true;
        //EnableJournal();
        //journal.SetActive(true);
        menuBackground.enabled = true;
        buttons.SetActive(true);
        StateController.cursorLocked = false;
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