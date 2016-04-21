using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class InspectViewToggle : MonoBehaviour
{
    public float transitionDuration = 0.5f;

    private CameraOrbit mouseOrbit;
    private PlayerController thirdChar;
    private PlayerInputHandler thirdContr;
    private GameObject player;
    private new Camera camera;
    private Animator anim;
    [HideInInspector]
    public static bool FirstPerson { get; private set; }
    private Transform inspectTarget;
    private Transform thirdPersonTarget;
    private bool firstTimeTP;
    private bool firstTimeFP;
    private bool resetOnceFP;
    private bool resetOnceTP;
    private float startTime;

    private bool isInspecting = false;
    void Awake()
    {
        player = GameObject.Find("Player");
        if (player == null)
            Debug.LogError("InspectViewToggle (" + transform.name + ") can not find Player.");
        inspectTarget = GameObject.Find("Player/CameraReferences/InspectTarget").transform;
        if (inspectTarget == null)
            Debug.LogError("InspectViewToggle (" + transform.name + ") can not find Player/CameraReferences/InspectTarget.");
        thirdPersonTarget = GameObject.Find("ThirdPersonTarget").transform;
        if (thirdPersonTarget == null)
            Debug.LogError("InspectViewToggle (" + transform.name + ") can not find ThirdPersonTarget.");
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

        camera.enabled = true;
    }
    public void StartInspectView()
    {
        isInspecting = true;
    }
    public void ExitInspectView()
    {
        isInspecting = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isInspecting && StateController.currentView != CameraStatus.FirstPersonView)
        {
            //Operations that only need/should be executed once every new right click
            if (firstTimeFP)
            {
                StateController.currentView = CameraStatus.InspectView;

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

            //TODO1: If the right mouse button is spammed, the transision might act really wierd.
            //Still time left on the transision?
            if ((Time.time - startTime) < transitionDuration)
            {
                StartCoroutine(Transition(inspectTarget));
                StartCoroutine(RotateOverTime(inspectTarget));
            }
            else
            {//Camera transision complete

                //Operations that only need/should be executed once when the transision is complete
                if (resetOnceFP)
                {

                    //Switch to first person view controlls and first person movement and activates lens if player is in light
                    FirstPerson = true;
                    anim.SetBool("firstPerson", FirstPerson);

                    //Resets the rotation of the camera to the player rotation
                    //TODO: Is this needed?
                    transform.eulerAngles = inspectTarget.transform.eulerAngles;

                    //Make the camera a child to the parent
                    transform.parent = player.transform;
                    //transform.localPosition = player.transform.Find("FirstPersonTarget").transform.localPosition;
                    transform.localPosition = inspectTarget.transform.localPosition;

                    //If the coroutines are not stopped manually, jitter will apear 
                    StopAllCoroutines();

                    resetOnceFP = false;
                }
                /*if( press button) TODO5
				{ 
					//lens.parent = null;
					//lens.component<collider> activate
				}
				*/
            }
        }
        else if (!isInspecting && StateController.currentView != CameraStatus.FirstPersonView)
        {
            //Operations that only need/should be executed once every right click release
            if (firstTimeTP)
            {
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

                //Switch to first person view controlls and first person movement and turns lens off if player is in light
                FirstPerson = false;
                anim.SetBool("firstPerson", FirstPerson);

                //Remove the parent
                transform.parent = null;

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
