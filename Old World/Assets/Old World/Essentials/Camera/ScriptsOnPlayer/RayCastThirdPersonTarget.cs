using UnityEngine;
using System.Collections;

public class RayCastThirdPersonTarget : MonoBehaviour {

    public static float distance3 = 5;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, ~0 , QueryTriggerInteraction.Ignore))
        {
            if (hit.distance > 5)
                distance3 = RaycastCamera.maxDistance;
            else
                distance3 = hit.distance;
        }
        else
            distance3 = RaycastCamera.maxDistance;
    }
}