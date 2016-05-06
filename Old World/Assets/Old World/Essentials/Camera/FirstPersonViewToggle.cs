using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class FirstPersonViewToggle : MonoBehaviour
{
    public float transitionDuration = 0.5f;

    private CameraOrbit mouseOrbit;
    private PlayerController thirdChar;
    private PlayerInputHandler thirdContr;
    private GameObject player;
    private Crosshair crosshair;
    private new Camera camera;
    private Animator anim;
    [HideInInspector]
    public static bool FirstPerson{ get; private set; }
    private Transform firstPersonTarget;
    private Transform thirdPersonTarget;
    private bool firstTimeTP;
    private bool firstTimeFP;
    private bool resetOnceFP;
    private bool resetOnceTP;
    private float startTime;
    
    void Awake()
    {
        player = GameObject.Find("Player");
        if (player == null)
            Debug.LogError("FirstPersonToggle (" + transform.name + ") can not find Player.");
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        if (crosshair == null)
            Debug.LogError("FirstPersonToggle (" + transform.name + ") can not find Crosshair.");
        firstPersonTarget = GameObject.Find("Player/CameraReferences/FirstPersonTarget").transform;
        if (firstPersonTarget == null)
            Debug.LogError("FirstPersonToggle (" + transform.name + ") can not find Player/CameraReferences/FirstPersonTarget.");
        thirdPersonTarget = GameObject.Find("ThirdPersonTarget").transform;
        if (thirdPersonTarget == null)
            Debug.LogError("FirstPersonToggle (" + transform.name + ") can not find ThirdPersonTarget.");
    }
    // Use this for initialization
    void Start()
    {
        firstTimeFP = true;
        firstTimeTP = true;
        resetOnceFP = true;
        resetOnceTP = true;

        camera = GetComponent<Camera>();
        //player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();

        mouseOrbit = camera.GetComponent<CameraOrbit>();
        mouseOrbit.enabled = true;

        thirdChar = player.GetComponent<PlayerController>();
        thirdChar.enabled = true;

        thirdContr = player.GetComponent<PlayerInputHandler>();
        thirdContr.enabled = true;
        crosshair.enabled = false;

        camera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && StateController.currentView != CameraStatus.InspectView)
        {
            //Rotate the player towards the cameras viewpoint
            player.transform.forward = camera.transform.forward;
            player.transform.eulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);

            //Operations that only need/should be executed once every new right click
            if (firstTimeFP)
            {
                StateController.currentZoom = ZoomStatus.zoomingIn;
                StateController.currentView = CameraStatus.FirstPersonView;
                firstTimeFP = false;

                //Enable first time enter operation on next mouse release
                firstTimeTP = true;
                resetOnceTP = true;

                //If the button was pressed too early set the new transistionDuration to the time since it was released
                if ((Time.time - startTime) < transitionDuration)
                {
                    StopAllCoroutines();
                    transitionDuration = (Time.time - startTime);
                }
                else
                {
                    transitionDuration = 0.5f;
                }
                startTime = Time.time;

                //Prevents the camera from rotating during camera transisions. This will remove unwanted camera snapping.
                thirdContr.setAllowCamera(false);

                mouseOrbit.enabled = false;
            }

            firstPersonTarget.transform.forward = camera.transform.forward;

            //TODO1: If the right mouse button is spammed, the transision might act really wierd.
            //Still time left on the transition?
            if ((Time.time - startTime) < transitionDuration)
            {
                StartCoroutine(Transition(firstPersonTarget));
                StartCoroutine(RotateOverTime(firstPersonTarget));
            }
            else
            {//Camera transition complete

                //Operations that only need/should be executed once when the transision is complete
                if (resetOnceFP)
                {

                    //Switch to first person view controlls and first person movement and activates lens if player is in light
                    FirstPerson = true;
                    anim.SetBool("firstPerson", FirstPerson);

                    //Resets the rotation of the camera to the player rotation
                    //TODO: Is this needed?
                    //transform.eulerAngles = player.transform.eulerAngles;

                    //Make the camera a child to the parent
                    transform.parent = player.transform;

                    //transform.localPosition = player.transform.Find("FirstPersonTarget").transform.localPosition;
                    transform.localPosition = firstPersonTarget.transform.localPosition;

                    //Make crosshair visable
                    crosshair.enabled = true;

                    //Allow the player to rotate the camera again
                    thirdContr.setAllowCamera(true);

                    //If the coroutines are not stopped manually, jitter will apear 
                    StopAllCoroutines();

                    resetOnceFP = false;
                }
            }
        }
        else if (!Input.GetMouseButton(1) && StateController.currentView != CameraStatus.InspectView)
        {
            //Operations that only need/should be executed once every right click release
            if (firstTimeTP)
            {
                StateController.currentZoom = ZoomStatus.zoomingOut;
                firstTimeTP = false;

                //Enable first time enter operations on next right click
                firstTimeFP = true;
                resetOnceFP = true;

                //TODO: If the right mouse button is spammed, the transision might act really wierd.
                //If the button was released too early 
                if ((Time.time - startTime) < transitionDuration)
                {
                    StopAllCoroutines();
                    transitionDuration = (Time.time - startTime);
                }
                else
                {
                    transitionDuration = 0.5f;
                }
                startTime = Time.time;

                //Switch to first person view controls and first person movement and turns lens off if player is in light
                FirstPerson = false;
                anim.SetBool("FirstPerson", FirstPerson);

                //Remove the parent
                transform.parent = null;

                //Disable crosshair
                crosshair.enabled = false;

                //Locking the camera movement to prevent unwanted camera snapping
                thirdContr.setAllowCamera(false);
            }

            //Still time left on the transision?
            if ((Time.time - startTime) < transitionDuration)
            {
                StartCoroutine(Transition(thirdPersonTarget));
                StartCoroutine(RotateOverTime(thirdPersonTarget));
            }
            else
            {//Camera transision complete
                //Operations that only need/should be executed once when the transision is complete
                if (resetOnceTP)
                {
                    StateController.currentView = CameraStatus.ThirdPersonView;
                    resetOnceTP = false;

                    //Allow the player to rotate the camera again
                    thirdContr.setAllowCamera(true);
                    //If the coroutines are not stopped manueally, jitter will apear 
                    StopAllCoroutines();

                    //Resets the rotation when switching back to thirdperson view
                    transform.eulerAngles = player.transform.eulerAngles;

                    //Resets the Orbit camera location so that it is positioned where it should be when switching back to third person view.
                    mouseOrbit.resetPosition();

                    //Keep this line at the end of the if-statement to prevent unwanted camera movement
                    mouseOrbit.enabled = true;
                }
            }
        }
    }


    IEnumerator Transition(Transform target)
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);

            transform.position = Vector3.Lerp(startingPos, target.position, t);
            yield return 0;
        }
    }

    IEnumerator RotateOverTime(Transform trans)
    {
        float t = 0.0f;
        Quaternion fromAngle = transform.rotation;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            
            transform.rotation = Quaternion.Lerp(fromAngle, Quaternion.Euler(trans.eulerAngles), t);
            yield return null;
        }
    }

}
