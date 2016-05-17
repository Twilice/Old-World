using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressureScript : MonoBehaviour
{
	public List<GameObject> Targets;
	
	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;

	private Vector3 StartPos;
	private Vector3 EndPos;
	private bool StandingOn = false;

	void Awake()
	{
		StartPos = transform.localPosition;
		EndPos = new Vector3(0, 0, 0);
	}

	void Update()
	{
		if (transform.localPosition != StartPos && StandingOn == false)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, StartPos, Time.deltaTime);
		}
	}

	void OnTriggerStay(Collider coll)
	{
		StandingOn = true;
		if (transform.localPosition != EndPos)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, EndPos, Time.deltaTime);
		}

		if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Lens"))
		{
			for (int i = 0; i < Targets.Count; i++)
			{
				if (PlatformTarget == true)
				{
					foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
					{
						movingScript.Activate();
					}
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
		StandingOn = false;
		if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Lens"))
        {
			for (int i = 0; i < Targets.Count; i++)
			{
				foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
				{
					if (movingScript.ReturnToOriginalPosition == true)
					{
						movingScript.returning = true;
					}
				}
				//if (ChargerTarget == true)
				//{
				//	Targets[i].GetComponent<ChargerScript>().enabled = false;
				//}
			}
		}
		
	}
}
