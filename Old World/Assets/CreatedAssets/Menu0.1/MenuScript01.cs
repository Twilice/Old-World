using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript01 : MonoBehaviour 
{
    private GameObject book;
    [Header("DisablesInMenu")] // only put always active scripts here
    public GameObject[] objects;
    private bool[] wasActive;

    // hardcoded script to disable in menu
    private CameraOrbit cameraOrbit; 
    private FirstPersonViewToggle cameraViewToggle;

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
        book = gameObject.FindChildObject("Journal");

        book.SetActive(true);
    }
	void Start() 
    {
        GameObject camera = GameObject.Find("MainCamera");
        cameraOrbit = camera.GetComponent<CameraOrbit>();
        cameraViewToggle = camera.GetComponent<FirstPersonViewToggle>();

        book.SetActive(false);
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
                openJournal = false;
                cameraOrbit.enabled = true;
                cameraViewToggle.enabled = true;
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].SetActive(wasActive[i]);
                }
                Time.timeScale = 1.0f;
            }
            else
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
                Time.timeScale = 0.0f;
            }
        }

        if (openJournal && !oldOpenJournal)
        {
            if (book.activeSelf)
                book.SetActive(false);
            else
                book.SetActive(true);
        }
	}

}