using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class DoorScript : MonoBehaviour
{
    //private float t = 0.0f;
    private MovingPlatformScript[] mps;
    private bool active = false;

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
		//TODO: FIX DIS SHIT

		//gameObject.GetComponent<MovingPlatformScript>().Activate();
		//for (int i = 0; i < mps.Length; i++)
		//{
		//    mps[i].Activate();
		//}
		//Debug.Log("Activating door");
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
