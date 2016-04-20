using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class DoorScript : MonoBehaviour
{
    private float t = 0.0f;
    void Awake()
    {
    }

    void Update()
    {
        if (RoomState.roomFullyPowered)
        {
            Activate();
            //Quaternion fromAngle = transform.rotation;
        }
    }

    void Activate()
    {
        gameObject.GetComponent<MovingPlatformScript>().Activate();
    }
}
