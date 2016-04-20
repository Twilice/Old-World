using UnityEngine;
using System.Collections;

public class ThirdPersonZoomOutTarget : MonoBehaviour {

    [HideInInspector]
    public float ThirdPersonZoomOutDistance = 5.0f;

    private Rigidbody rb;
    private GameObject player;
    private Transform target;

    void Awake()
    {
        target = GameObject.Find("Player/CameraReferences/CameraRotationReference").transform;
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        if (target)
        {
            Quaternion rotation = Quaternion.Euler(25, player.transform.rotation.eulerAngles.y, 0);

            //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -ThirdPersonZoomOutDistance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    void Update()
    {
        //Updating camera distance on every frame
        ThirdPersonZoomOutDistance = RayCastThirdPersonTarget.distance3;
    }
}
