using UnityEngine;
using System.Collections;

public class RaycastCamera : MonoBehaviour
{
    public static float distance3 = 5;
    public static float maxDistance = 8;
    void Update()
    {
        RaycastHit hit;
        Vector3 topRightCorner = transform.position + transform.up *0.5f + transform.right * 0.5f;
        Vector3 bottomLeftCorner = transform.position - transform.up * 0.5f - transform.right * 0.5f;
        Vector3 bottomRightCorner = transform.position - transform.up * 0.5f + transform.right * 0.5f;
        Vector3 topLeftCorner = transform.position + transform.up * 0.5f - transform.right * 0.5f;
        float distance = maxDistance;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance);
        }

        if (Physics.Raycast(topRightCorner, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance);
        }

        if (Physics.Raycast(bottomLeftCorner, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance);
        }

        if (Physics.Raycast(bottomRightCorner, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance);
        }

        if (Physics.Raycast(topLeftCorner, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance);
        }

        distance3 = distance;




        /*if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            if (hit.distance > maxDistance)
                distance3 = maxDistance;
            else
                distance3 = hit.distance;
        }
        else
            distance3 = maxDistance;*/
    }
}