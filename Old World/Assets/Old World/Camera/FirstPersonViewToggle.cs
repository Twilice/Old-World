using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Characters.FirstPerson;

public class FirstPersonViewToggle : MonoBehaviour {
    private MouseOrbitImproved mouseOrbit;
    private ThirdPersonCharacter thirdChar;
    private ThirdPersonUserControl thirdContr;
    private RigidbodyFirstPersonController rbFirstContr;
    public GameObject player;
    public Crosshair crosshair;
    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;

    // Use this for initialization
    void Start () {

        mouseOrbit = thirdPersonCamera.GetComponent<MouseOrbitImproved>();
        mouseOrbit.enabled = true;

        thirdChar = player.GetComponent<ThirdPersonCharacter>();
        thirdChar.enabled = true;

        thirdContr = player.GetComponent<ThirdPersonUserControl>();
        thirdContr.enabled = true;

        rbFirstContr = player.GetComponent<RigidbodyFirstPersonController>();
        rbFirstContr.enabled = false;
        crosshair.enabled = false;

        firstPersonCamera.enabled = false;
        firstPersonCamera.GetComponent<AudioListener>().enabled = false;
        thirdPersonCamera.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(1))
        {
            mouseOrbit.enabled = false;
            thirdChar.enabled = false;
            thirdContr.enabled = false;
            rbFirstContr.enabled = true;
            ToggleCamera(firstPersonCamera);
            ToggleCamera(thirdPersonCamera);
            crosshair.enabled = true;

            
        } else if (Input.GetMouseButtonUp(1))
        {
            mouseOrbit.enabled = true;
            thirdChar.enabled = true;
            thirdContr.enabled = true;
            rbFirstContr.enabled = false;
            ToggleCamera(firstPersonCamera);
            ToggleCamera(thirdPersonCamera);
            crosshair.enabled = false;
        }
	}

    void ToggleCamera(Camera c)
    {
        c.enabled = !c.enabled;
        c.GetComponent<AudioListener>().enabled = c.enabled;
    }
}
