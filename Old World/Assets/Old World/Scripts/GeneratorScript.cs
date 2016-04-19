using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorScript : MonoBehaviour
{
	public List<GameObject> Targets;

	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;

	public bool Active = false;

	void Start ()
	{
	}

	void Update()
	{
		if (Active == true)
		{
			for (int i = 0; i < Targets.Count; i++)
			{
				if (PlatformTarget == true)
				{
					Targets[i].GetComponent<MovingPlatformScript>().Activate();
				}
				//if (ChargerTarget == true)
				//{
				//	Targets[i].GetComponent<ChargerScript>().Activate();
				//}
			}
		}
	}

	public void Activate()
	{
		GetComponent<Renderer>().material.color = Color.green;
		Active = true;
		for (int i = 0; i < Targets.Count; i++)
		{
			if (GeneratorTarget == true)
			{
				Targets[i].GetComponent<GeneratorScript>().Activate();
			}
		}
	}
}
