using UnityEngine;
using System.Collections;

public class ThridPersonTargetRayCast : MonoBehaviour {

    public float distance = 5.0f;
    public float mouseSensitivity = 1.75f;
    public float joyStickSensitivityX = 2f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rb;
    private GameObject player;
    private Transform target;

    void Awake()
    {
        target = GameObject.Find("Player/CameraReference").transform;
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");

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
            Quaternion rotation = Quaternion.Euler(25, player.transform.rotation.eulerAngles.y, 0);

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
        distance = RayCastTarget.distance3;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
