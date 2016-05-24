using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebuggerScript : MonoBehaviour
{
	private MovingPlatformScript[] Targets;
    private int NumberOfTargets;
	private int SelectedTarget;
	private GameObject holder;

	void Start ()
	{
		Targets = FindObjectsOfType<MovingPlatformScript>();

		NumberOfTargets = Targets.Length;
		//Debug.Log(NumberOfTargets);
	}
	
	void Update ()
	{
		//Debugger
		if (Input.GetKey(KeyCode.G))
		{
			Debug.Log("Activating");
			for (int i = 0; i < NumberOfTargets; i++)
			{
				//Targets[i].Speed = 10;
				//Targets[i].Elevator = true;
				Targets[i].Activate();
            }
		}

		if (Input.GetKey(KeyCode.T))
		{
			Debug.Log("Deactivating");
			for (int i = 0; i < NumberOfTargets; i++)
			{
				//Targets[i].Speed = 10;
				//Targets[i].Elevator = true;
				Targets[i].MovingBack();
			}
		}


		if (Input.GetKey(KeyCode.Space))
		{
			//Cycles targets
			SelectedTarget++;
			if (SelectedTarget >= NumberOfTargets)
			{
				SelectedTarget = 0;
			}

			//Activate target
			if (Targets[SelectedTarget].enabled == false)
			{
				Targets[SelectedTarget].enabled = true;
			}

				//Deactivate target
			else if (Targets[SelectedTarget].enabled == true)
			{
				Targets[SelectedTarget].enabled = false;
				//Debug.Log("Deactivating: " + SelectedTarget);
			}
		}
	}
}
