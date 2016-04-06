﻿using UnityEngine;
using System.Collections;

public class Raycast3 : MonoBehaviour
{
    public static float distance3 = 5;
    public float maxDistance = 5;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, 0, QueryTriggerInteraction.Ignore))
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