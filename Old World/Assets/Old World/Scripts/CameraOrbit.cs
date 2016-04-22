using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[AddComponentMenu("Camera-Control/Mouse Orbit")]

public class CameraOrbit : MonoBehaviour
{

    public float distance = 5.0f;
    public float mouseSensitivity = 1.75f;
    public float joyStickSensitivityX = 2f;

    public float yMinLimit = -60f;
    public float yMaxLimit = 80f;

    public float distanceMin = 0.5f;
    public float distanceMax = 15f;

    private Rigidbody rb;
    private Transform target;

    float x = 0.0f;
    float y = 0.0f;

    void Awake()
    {
        target = GameObject.Find("Player/CameraReferences/CameraRotationReference").transform;
        if (target == null)
            Debug.LogError("MouseOrbit (" + transform.name + ") can not find Player/CameraReferences/CameraRotationReference.");
    }

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rb = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            float mouseY = Input.GetAxisRaw("Mouse Y");
			
            float joyX = Input.GetAxisRaw("Joy X");
            float joyY = Input.GetAxisRaw("Joy Y");
            if (joyX != 0 || joyY != 0)
            {
                x += joyX * joyStickSensitivityX;
                y -= joyY;
            }
            else
            {
                x += mouseX * mouseSensitivity;
                y -= mouseY * mouseSensitivity;
            }

			RaycastCamera.maxDistance = Mathf.Clamp(RaycastCamera.maxDistance - (Input.GetAxis("Zoom") * 5), distanceMin, distanceMax);



			y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    void Update()
    {
        //Updating camera distance on every frame
        distance = RaycastCamera.distance3;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    //Resets the Orbit camera location so that it is positioned where it should be when switching back to third person view.
    public void resetPosition()
    {   
        x = transform.eulerAngles.y;
        y = 25;
    }
}