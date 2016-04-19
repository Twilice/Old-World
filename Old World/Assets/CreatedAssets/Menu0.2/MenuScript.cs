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
    private MouseOrbitImproved cameraOrbit; 
    private FirstPersonViewToggle cameraViewToggle;
    public MouseLook mouseToggle;

    private bool pause;
    private bool oldPause;
    private bool restart;
    private bool oldRestart;
    private bool openJournal;
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
        cameraOrbit = camera.GetComponent<MouseOrbitImproved>();
        cameraViewToggle = camera.GetComponent<FirstPersonViewToggle>();
        mouseToggle = MouseLook.GetMouseLook();

        journal.SetActive(false);
	}
	
	void Update() 
    {
        oldRestart = restart;
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
        }
	}

    private void DisableJournal()
    {
        openJournal = true;
        cameraOrbit.enabled = false;
        cameraViewToggle.enabled = false;
        for (int i = 0; i < objects.Length; i++)
        {
            wasActive[i] = objects[i].activeSelf;
            Debug.Log(wasActive[i]);
            objects[i].SetActive(false);
        }
    }

    private void EnableJournal()
    {
        openJournal = false;
        cameraOrbit.enabled = true;
        cameraViewToggle.enabled = true;
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(wasActive[i]);
        }
    }

}