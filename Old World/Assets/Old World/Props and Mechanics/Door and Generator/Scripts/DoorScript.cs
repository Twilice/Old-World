using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class DoorScript : MonoBehaviour
{
    //private float t = 0.0f;
    private MovingPlatformScript[] mps;

    void Awake()
    {
        mps = GetComponentsInChildren<MovingPlatformScript>();
    }

    void Update()
    {
        if (StateController.roomFullyPowered)
        {
            Invoke("Activate", 5);
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
}
