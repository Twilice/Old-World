using UnityEngine;
using System.Collections;

public class RaycastCamera : MonoBehaviour
{
    public static float distance3 = 5;
    public static float maxDistance = 8;
    private int layerMask;
    public float raycastSize = 0.35f;
    public float collisionOffset = 0.1f;
    void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Player");
        layerMask = ~layerMask;
    }
    void Update()
    {
        RaycastHit hit;
        Vector3 topRightCorner = transform.position + transform.up * raycastSize + transform.right * raycastSize;
        Vector3 bottomLeftCorner = transform.position - transform.up * raycastSize - transform.right * raycastSize;
        Vector3 bottomRightCorner = transform.position - transform.up * raycastSize + transform.right * raycastSize;
        Vector3 topLeftCorner = transform.position + transform.up * raycastSize - transform.right * raycastSize;
        float distance = maxDistance;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance- collisionOffset);
        }

        if (Physics.Raycast(topRightCorner, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance- collisionOffset);
        }

        if (Physics.Raycast(bottomLeftCorner, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance- collisionOffset);
        }

        if (Physics.Raycast(bottomRightCorner, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance- collisionOffset);
        }

        if (Physics.Raycast(topLeftCorner, transform.TransformDirection(Vector3.forward), out hit, RaycastCamera.maxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            distance = Mathf.Min(distance, hit.distance- collisionOffset);
        }

        distance3 = distance;
    }
}