using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorScript : MonoBehaviour
{
	public List<GameObject> Targets;
	
	void Start ()
	{
	}
	
	void Update ()
	{
		
		for (int i = 0; i < Targets.Count; i++)
		{
			Targets[i].GetComponent<MovingPlatformScript>().enabled = true;
			Targets[i].GetComponent<GeneratorScript>().enabled = true;
			Targets[i].GetComponent<ChargerScript>().enabled = true;
		}
	}
}
