using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerController m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    private Camera firstPersonCamera;
    private float lastTime;
    private bool allowCameraMovement = false; //Used to lock first person camera and player rotation during camera transisions
    private CameraLookAt mouseLook = CameraLookAt.GetMouseLook();
	private PlayerController pController;
    private bool OpenMenuButton;
    private CameraOrbit CameraScript;
    private void Start()
    {
		pController = GameObject.Find("Player").GetComponent<PlayerController>();
		CameraScript = GameObject.Find("MainCamera").GetComponent<CameraOrbit>();
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogError("No MainCamera found.");
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<PlayerController>();
    }


    private void Update()
    {
        //If first person camera is toggled, allow first person camera movement
        if (FirstPersonViewToggle.FirstPerson)
        {
            //Only allow camera position correction if the button has been released for more than half a second.
            //This is to prevent the camera from locking the vertical axis and will cause the player to face the rotation that was
            //active when last leaving first person view.
            if (Time.time - lastTime > 0.5)
            {
                mouseLook.Init(transform);
            }
            lastTime = Time.time;

            //Rotate the camera with first person controlls
            //Not allowed if the camera is transitioning
            if (allowCameraMovement)
            {
                m_Character.RotateView();
            }
        }
        else if (!m_Jump)
        {
            m_Jump = Input.GetButtonDown("Jump");
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        //The if statement is needed to prevent the model from turning with the horizontal keys during a camera transision
        float h;
        float v;
        if (allowCameraMovement)
        {
			v = Input.GetAxis("Vertical");

			if (pController.m_IsGrounded)
			{
				h = Input.GetAxis("Horizontal");
			}
			else
			{
				h = Input.GetAxis("Horizontal") * 0.5f;
			}

		}
        else
        {
			//Prevents the character from turning backwards during camera transisions
			h = 0.0f;
            if (Input.GetAxis("Vertical") < 0)
                v = 0.0f;
            else
                v = Input.GetAxis("Vertical");
        }


        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }

        // pass all parameters to the character control script
        // TODO crouch should not exist?
        if (StateController.currentView != CameraStatus.InspectView)
            m_Character.Move(m_Move, crouch, m_Jump);
        else m_Character.Move(Vector3.zero, false, false);
        m_Jump = false;
    }


    public void setAllowCamera(bool x)
    {
        allowCameraMovement = x;
    }
}