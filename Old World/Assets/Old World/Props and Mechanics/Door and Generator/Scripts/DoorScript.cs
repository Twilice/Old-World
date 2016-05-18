using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class DoorScript : MonoBehaviour
{
    //private float t = 0.0f;
    private MovingPlatformScript[] mps;
    [Header("If active without power")]
    public bool active = false;

    void Awake()
    {
        mps = GetComponentsInChildren<MovingPlatformScript>();
    }

    void Update()
    {
        if (StateController.roomFullyPowered)
        {
            active = true;
        }
    }

    void Activate()
    {
		foreach (MovingPlatformScript movingScript in mps)
		{
			movingScript.Activate();
		}
	}

    void OnTriggerStay()
    {
        if(active)
            foreach (MovingPlatformScript movingScript in mps)
            {
                movingScript.returning = false;
                Activate();
            }
    }

    void OnTriggerExit()
    {
        if(active)
            foreach (MovingPlatformScript movingScript in mps)
            {
                movingScript.returning = true;
            }
    }
}
