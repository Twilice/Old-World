﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BatterySlotLinked : MonoBehaviour
{
	[HideInInspector]
	public bool online = false;

	public bool hasBattery;

	public List<GameObject> Targets;

	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;
	public BatterySlotLinked partner;

	void Update()
	{
		if (hasBattery == true && gameObject.GetComponentInChildren<Battery>().amountOfCharge > 0)
		{
			online = true;
			if (partner.online == true)
			{
				for (int i = 0; i < Targets.Count; i++)
				{
					if (hasBattery == false)
					{
						break;
					}

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

					if (ChargerTarget == true)
					{
						Targets[i].GetComponent<ChargerScript>().Activate();
					}
				}
			}
		}

		if (hasBattery == false)
		{
			for (int p = 0; p < Targets.Count; p++)
			{
				foreach (MovingPlatformScript movingScript in Targets[p].GetComponentsInChildren<MovingPlatformScript>())
				{
					if (movingScript.ReturnToOriginalPosition == true)
					{
						movingScript.returning = true;
					}
				}
			}
		}
	}

	void OnCollisionExit(Collision coll)
	{
		if (coll.gameObject.CompareTag("Battery"))
		{
			hasBattery = false;
			online = false;
		}
	}
}