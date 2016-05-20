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
    private bool allowCameraMovement = true; //Used to lock first person camera and player rotation during camera transisions
    private bool OpenMenuButton;

    public Vector3 rotatedAmmount { get; private set; }
    private Camera mainCamera;
    private void Start()
    {

        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        LookInit(transform, mainCamera.transform);
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
                LookInit(transform, m_Cam);
            }
            lastTime = Time.time;

            //Rotate the camera with first person controlls
            //Not allowed if the camera is transitioning
            if (allowCameraMovement)
            {
                RotateView();
            }
        }
        else if (!m_Jump)
        {
            m_Jump = Input.GetButtonDown("Jump");
        }


        // read inputs
        //The if statement is needed to prevent the model from turning with the horizontal keys during a camera transision
        float h;
        float v;
        if (allowCameraMovement)
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
            //Debug.Log("h" + h); //input manager gets 50% nuked on sceneswitch, hoppas controller funkar bättre än keyboard
            //Debug.Log("v" + v);
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
        if (StateController.currentView == CameraStatus.ThirdPersonView)
            m_Character.Move(m_Move, m_Jump);
        else m_Character.Move(Vector3.zero, false);
        m_Jump = false;
    }

    public void setAllowCamera(bool x)
    {
        allowCameraMovement = x;
    }

    // below is emigrated mouseLook/cameraLookAt
    private float XSensitivity = 2f;
    private float YSensitivity = 2f;
    private float XJoySensitivty = 0.8f;
    private float YJoySensitivty = 0.7f;
    private bool clampVerticalRotation = true;
    private float MinimumX = -80F;
    private float MaximumX = 80F;
    private bool smooth = false;
    private float smoothTime = 5f;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;

    public void LookInit(Transform character, Transform camera)
    {
        m_CharacterTargetRot = character.localRotation;

        //We don't need the camera rotation, since it should always be straight
        //m_CameraTargetRot = Quaternion.identity;
        m_CameraTargetRot = camera.localRotation;
    }

    public void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

        rotatedAmmount = transform.rotation.eulerAngles;
        LookRotation(transform, mainCamera.transform);
        rotatedAmmount = transform.rotation.eulerAngles - rotatedAmmount;
    }

    public void LookRotation(Transform character, Transform camera)
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float joyX = Input.GetAxis("Joy X");
        float joyY = Input.GetAxis("Joy Y");

        float xRot;
        float yRot;
        if (joyX != 0 || joyY != 0)
        {
            xRot = joyX * XJoySensitivty;
            yRot = joyY * YJoySensitivty;
        }
        else
        {
            xRot = mouseX * XSensitivity;
            yRot = mouseY * YSensitivity;
        }

        m_CharacterTargetRot *= Quaternion.Euler(0f, xRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-yRot, 0f, 0f);

        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        if (smooth)
        {
            character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot, smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
        }
        else
        {
            character.localRotation = m_CharacterTargetRot;
            camera.transform.localRotation = m_CameraTargetRot;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

   
}
