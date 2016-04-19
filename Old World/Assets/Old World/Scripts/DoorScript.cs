using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour
{
	public List<GeneratorScript> Generators;

	private bool IsPowerered = false;
	
	void Start ()
	{
	}

	void Update ()
	{
		IsPowerered = true;
		for (int i = 0; i < Generators.Count; i++)
		{
			if (Generators[i].Active == false)
			{
				IsPowerered = false;
			}
		}

		if (IsPowerered == true)
		{
			Activate();
		}
	}

	void Activate()
	{
		gameObject.GetComponent<MovingPlatformScript>().Activate();
	}
}
