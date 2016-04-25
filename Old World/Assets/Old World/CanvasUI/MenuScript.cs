using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour 
{
    private GameObject journal;
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

        journal.SetActive(true);
    }
	void Start() 
    {
        GameObject camera = GameObject.Find("MainCamera");
        cameraOrbit = camera.GetComponent<CameraOrbit>();
        cameraViewToggle = camera.GetComponent<FirstPersonViewToggle>();
        //mouseToggle = CameraLookAt.GetMouseLook();

        journal.SetActive(false);
	}
	

    // TODO3 Journal not starting correctly
	void Update() 
    {
    /*    oldRestart = restart;
        restart = Input.GetButton("Restart");

        oldOpenJournal = openJournal;
        //openJournal = TODO5 "menu button" //= Input.GetButton("Journal");
       
        if (restart && !oldRestart)
            SceneManager.LoadScene("TestingScene");

        oldPause = pause;
        pause = Input.GetButton("Menu");

        //Pause game
        if (pause && !oldPause)
        {
            if (Time.timeScale == 0)
            {
                //mouseToggle.DeactivateMouse();
                Time.timeScale = 1.0f;
                EnableJournal();
            }
            else
            {
                //mouseToggle.ActiveateMouse();
                Time.timeScale = 0.0f;
                DisableJournal();
            }
        }

        if (openJournal && !oldOpenJournal)
        {
            if (journal.activeSelf)
                journal.SetActive(false);
            else
                journal.SetActive(true);
        }*/
	}

    public void OpenMenu()
    {
        Time.timeScale = 0.0f;
        EnableJournal();
        journal.SetActive(true);
        StateController.menuOpen = true;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
        DisableJournal();
        journal.SetActive(false);
        StateController.menuOpen = false;
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