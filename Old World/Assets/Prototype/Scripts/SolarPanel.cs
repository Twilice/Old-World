using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarPanel : TriggeredByLight
{
	public List<GameObject> Targets;

	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;

	public int ChargeUpTime;

	private bool Active = false;

	protected override void HitByLightStay()
	{
		if (timeIlluminated >= ChargeUpTime)
		{
			//Play sound once
			//Light on panel turned on

			Active = true;
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
