using UnityEngine;
using System.Collections;

public class RaycastCamera : MonoBehaviour
{
    public static float distance3 = 5;
    public float maxDistance = 5;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.distance > 5)
                distance3 = maxDistance;
            else
                distance3 = hit.distance;
        }
        else
            distance3 = maxDistance;
    }
}