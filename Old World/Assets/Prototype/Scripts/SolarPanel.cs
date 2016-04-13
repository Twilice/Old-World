using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarPanel : TriggeredByLight
{
	public List<GameObject> Targets;
	public int ChargeUpTime;

	private bool Active;

	protected override void HitByLightStay()
	{
		if (timeIlluminated >= ChargeUpTime)
		{
			//Play sound once
			//Light on panel turned on

			Active = true;
			for (int i = 0; i < Targets.Count; i++)
			{
				Targets[i].GetComponent<MovingPlatformScript>().enabled = true;
				Targets[i].GetComponent<GeneratorScript>().enabled = true;
				Targets[i].GetComponent<ChargerScript>().enabled = true;
			}
		}

		//else
		//{
		//	//Play powering up sound
		//	//Fade lights on?
		//}
	}

	protected override void HitByLightExit()
	{
		Active = false;
		for (int i = 0; i < Targets.Count; i++)
		{
			Targets[i].GetComponent<MovingPlatformScript>().enabled = false;
			Targets[i].GetComponent<ChargerScript>().enabled = false;
		}
	}

	void Start()
	{
		Active = false;
	}

	void Update()
	{

	}
}
