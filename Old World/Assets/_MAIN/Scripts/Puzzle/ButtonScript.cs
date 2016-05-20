using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonScript : MonoBehaviour
{
	public List<GameObject> Targets;
	public bool platformTarget;
	public bool generatorTarget;
	public bool chargerTarget;
	public bool Active = false;

//	private Vector3 StartPos;
	private Vector3 EndPos;

	void Awake ()
	{
	//	StartPos = transform.localPosition;
		EndPos = new Vector3(0, -0.1f, 0);
	}
	
	void Update ()
	{
		if (Active)
		{
			if (transform.localPosition != EndPos)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, EndPos, Time.deltaTime);
			}

			for (int i = 0; i < Targets.Count; i++)
			{
				if (platformTarget == true)
				{
					foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
					{
						movingScript.Activate();
					}
				}
				if (generatorTarget == true)
				{
					Targets[i].GetComponent<GeneratorScript>().Activate();
				}
				if (chargerTarget == true)
				{
					Targets[i].GetComponent<ChargerScript>().Activate();
				}
			}
		}
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Active != true)
		{
			Active = true;
		}
	}
}
