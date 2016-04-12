using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarPanel : MonoBehaviour//TriggeredByLight
{
	public List<GameObject> Targets;
	public int ChargeUpTime;

	private bool Active;

	void Start ()
	{
		Active = false;
    }
	
	void Update ()
	{
		if (Active == true)
		{
			//Light on panel turning on?
		}
	}

	void HitByLightStay()
	{
		//if (timeIlluminated >= ChargeUpTime)
		//{
			//Play sound once
			Active = true;
			for (int i = 0; i < Targets.Count; i++)
			{
				Targets[i].GetComponent<MovingPlatformScript>().enabled = true;
				Targets[i].GetComponent<GeneratorScript>().enabled = true;
				Targets[i].GetComponent<ChargerScript>().enabled = true;
			}
		//}

		//else
		//{
		//	//Play powering up sound
		//	//Fade lights on?
		//}
	}
}
