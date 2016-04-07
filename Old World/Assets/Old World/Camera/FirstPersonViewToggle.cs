using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class FirstPersonViewToggle : MonoBehaviour
{
    private MouseOrbitImproved mouseOrbit;
    private ThirdPersonCharacter thirdChar;
    private ThirdPersonUserControl thirdContr;
    public GameObject player;
    public Transform firstPersonCameraPosition;
    public Crosshair crosshair;
    private new Camera camera;
    private Animator anim;
    private Transform savedTrans;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();

        mouseOrbit = camera.GetComponent<MouseOrbitImproved>();
        mouseOrbit.enabled = true;

        thirdChar = player.GetComponent<ThirdPersonCharacter>();
        thirdChar.enabled = true;

        thirdContr = player.GetComponent<ThirdPersonUserControl>();
        thirdContr.enabled = true;
        crosshair.enabled = false;

        camera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouseOrbit.enabled = false;

            //Change the camera position
            transform.eulerAngles = player.transform.eulerAngles;

            //Make the camera a child to the parent
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0.25f, 1.42f, -0.65f);
            
            crosshair.enabled = true;
            anim.SetBool("firstPerson", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            //Remove the parent
            transform.parent = null;

            //Enable Mouse Orbit
            mouseOrbit.enabled = true;
                   
            //CHANGE CAMERA POSITION TO SAVED THIRD PERSON VIEW

            crosshair.enabled = false;
            anim.SetBool("firstPerson", false);
        }
    }


}
