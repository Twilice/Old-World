using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BatterySlot : MonoBehaviour
{
	[HideInInspector]
	public bool hasBattery = false;

	public List<GameObject> Targets;
	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;
	
	void Update ()
	{
		if (hasBattery == true)
		{
			for (int i = 0; i < Targets.Count; i++)
			{
				gameObject.GetComponentInChildren<Battery>().amountOfCharge--;
				if (gameObject.GetComponentInChildren<Battery>().amountOfCharge <= 0)
				{
					hasBattery = false;
					for (int p = 0; p < Targets.Count; p++)
					{
						if (Targets[p].GetComponent<MovingPlatformScript>().ReturnToOriginalPosition == true)
						{
							Targets[p].GetComponent<MovingPlatformScript>().Deactivate();
						}
					}
					break;
				}
				if (PlatformTarget == true)
				{
					Targets[i].GetComponent<MovingPlatformScript>().Activate();
				}
				if (GeneratorTarget == true)
				{
					Targets[i].GetComponent<GeneratorScript>().Activate();
				}
				if (ChargerTarget == true)
				{
					Targets[i].GetComponent<ChargerScript>().Activate();
				}
			}
		}
	}

	void OnCollisionExit(Collision coll)
	{
		if (coll.gameObject.CompareTag("Battery"))
		{
			hasBattery = false;
		}
	}
}
