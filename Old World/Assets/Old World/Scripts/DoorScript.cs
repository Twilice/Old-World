using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class DoorScript : MonoBehaviour
{
    private EmissionIntensityController[] e;
    private float t = 0.0f;
    void Awake()
    {
        e = FindObjectsOfType<EmissionIntensityController>();
        for (int i = 0; i < e.Length; i++)
        {
            Debug.Log(e[i]);
        }
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

    void LerpLight(EmissionIntensityController e)
    {
        if (t < 1.0f)
        {
            t += Time.deltaTime * 0.06f; // TODO5 timescale or something is wrong
            e.SetEmissionIntesity(Mathf.Lerp(0f, 2.8f, t));;
        }
    }
}
