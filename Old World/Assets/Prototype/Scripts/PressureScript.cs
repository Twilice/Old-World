using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressureScript : MonoBehaviour
{
	public List<GameObject> Targets;
	
	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Lens"))
		{
			for (int i = 0; i < Targets.Count; i++)
			{
				if (PlatformTarget == true)
				{
					Targets[i].GetComponent<MovingPlatformScript>().Activate();
				}
				if (GeneratorTarget == true)
				{
					Targets[i].GetComponent<GeneratorScript>().Activate();
				}
				//if (ChargerTarget == true)
				//{
				//	Targets[i].GetComponent<ChargerScript>().Activate();
				//}
			}
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Lens"))
        {
			for (int i = 0; i < Targets.Count; i++)
			{
				if (PlatformTarget == true)
				{
					Targets[i].GetComponent<MovingPlatformScript>().enabled = false;
				}
				//if (ChargerTarget == true)
				//{
				//	Targets[i].GetComponent<ChargerScript>().enabled = false;
				//}
			}
		}
		
	}
}
