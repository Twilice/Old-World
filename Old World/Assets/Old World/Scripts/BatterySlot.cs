﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BatterySlot : MonoBehaviour
{
	public bool hasBattery;

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
				if (gameObject.GetComponentInChildren<Battery>().amountOfCharge <= 0)
				{
					for (int p = 0; p < Targets.Count; p++)
					{
						foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
						{
							if (movingScript.ReturnToOriginalPosition == true)
							{
								movingScript.returning = true;
							}
						}
					}
					hasBattery = false;
					break;
				}

				gameObject.GetComponentInChildren<Battery>().amountOfCharge--;
				
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
}
