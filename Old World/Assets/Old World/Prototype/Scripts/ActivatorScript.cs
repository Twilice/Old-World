using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivatorScript : MonoBehaviour
{
	public List<GameObject> Targets;
	
	private int NumberOfTargets;
	private int SelectedTarget;

	void Start ()
	{
		NumberOfTargets = Targets.Count;
		for (int i = 0; i < NumberOfTargets; i++)
		{
			Targets[i].GetComponent<MovingPlatformScript>().enabled = false;
		}
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			
			SelectedTarget++;
			if (SelectedTarget >= NumberOfTargets)
			{
				SelectedTarget = 0;
			}

			

			if (Targets[SelectedTarget].GetComponent<MovingPlatformScript>().enabled == false)
			{
				Targets[SelectedTarget].GetComponent<MovingPlatformScript>().enabled = true;
				Debug.Log("Activating: " + SelectedTarget);
			}
			else if (Targets[SelectedTarget].GetComponent<MovingPlatformScript>().enabled == true)
			{
				Targets[SelectedTarget].GetComponent<MovingPlatformScript>().enabled = false;
				Debug.Log("Deactivating: " + SelectedTarget);
			}
        }
	}
}
