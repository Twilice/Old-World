using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour 
{
    private GameObject journal;
    private GameObject menuBackground;
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
        menuBackground = gameObject.FindChildObject("MenuBackground");
        journal.SetActive(true); //minns inte exakt varför jag sätter dem till active här, tror det var någon initieringsbug
        menuBackground.SetActive(true);
    }
	void Start() 
    {
        GameObject camera = GameObject.Find("MainCamera");
        cameraOrbit = camera.GetComponent<CameraOrbit>();
        cameraViewToggle = camera.GetComponent<FirstPersonViewToggle>();

        menuBackground.SetActive(false);
        journal.SetActive(false);
	}

    public void OpenMenu()
    {
        Time.timeScale = 0.0f;
        EnableJournal();
        journal.SetActive(true);
        menuBackground.SetActive(true);
        StateController.menuOpen = true;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
        DisableJournal();
        journal.SetActive(false);
        menuBackground.SetActive(false);
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