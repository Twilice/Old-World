﻿using UnityEngine;
using System.Collections;

public class RayCastTarget : MonoBehaviour {

    public static float distance3 = 5;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
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