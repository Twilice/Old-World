using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class FirstPersonViewToggle : MonoBehaviour
{
    private MouseOrbitImproved mouseOrbit;
    private ThirdPersonCharacter thirdChar;
    private ThirdPersonUserControl thirdContr;
    public GameObject player;
    public Crosshair crosshair;
    private new Camera camera;
    private Animator anim;
    public Transform firstPersonTarget;
    public Transform thirdPersonTarget;
    private bool firstTimeTP;
    private bool firstTimeFP;
    private float startTime;
    public float transitionDuration = 0.5f;

    // Use this for initialization
    void Start()
    {
        firstTimeFP = true;
        firstTimeTP = true;
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
        if (Input.GetMouseButton(1))
        {
            //Operations that only need/should be executed once every new right click
            if (firstTimeFP)
            {
                firstTimeFP = false;

                //Enable first time enter operation on next mouse release
                firstTimeTP = true;
                //If the button was pressed too early set the new transistionDuration to the time since it was released
                if ((Time.time - startTime) < transitionDuration)
                {
                    transitionDuration = (Time.time - startTime);
                }
                else
                {
                    transitionDuration = 0.5f;
                }
                startTime = Time.time;
                mouseOrbit.enabled = false;
            }

            if ((Time.time - startTime) < transitionDuration)
            {
                StartCoroutine(Transition(firstPersonTarget));
                StartCoroutine(RotateOverTime(firstPersonTarget));
            }
            else
            {
                anim.SetBool("firstPerson", true);
                crosshair.enabled = true;
                //Change the camera position
                transform.eulerAngles = player.transform.eulerAngles;

                //Make the camera a child to the parent
                transform.parent = player.transform;
                transform.localPosition = new Vector3(0.25f, 1.42f, -0.65f);
            }
        }
        else if (!Input.GetMouseButton(1))
        {
            //Operations that only need/should be executed once every right click release
            if (firstTimeTP)
            {
                firstTimeTP = false;

                //Enable first time enter operations on next right click
                firstTimeFP = true;

                transform.eulerAngles = player.transform.eulerAngles;

                //If the button was released too early 
                if ((Time.time - startTime) < transitionDuration)
                {
                    transitionDuration = (Time.time - startTime);
                }
                else
                {
                    transitionDuration = 0.5f;
                }
                startTime = Time.time;

                //Remove the parent
                transform.parent = null;
                

                //Disable crosshair
                crosshair.enabled = false;
            }

            if ((Time.time - startTime) < transitionDuration)
            {
                StartCoroutine(Transition(thirdPersonTarget));
                StartCoroutine(RotateOverTime(thirdPersonTarget));
            }
            else
            {
                //Enable Mouse Orbit
                mouseOrbit.enabled = true;
                anim.SetBool("firstPerson", false);
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
        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(trans.eulerAngles);
        for (float t = 0f; t < 1f; t += Time.deltaTime / transitionDuration)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

}
