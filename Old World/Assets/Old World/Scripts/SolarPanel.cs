using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarPanel : TriggeredByLight
{
	public List<GameObject> Targets;
	public GameObject Power_bar;

	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;

	public float ChargeUpTime;

	private bool Active = false;

	protected override void HitByLightStay()
	{
		Debug.Log("Hit by light");
		Power_bar.GetComponent<Charge_bar>().PowerTurnedOn(ChargeUpTime);
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
		Power_bar.GetComponent<Charge_bar>().ChangingColor.g = 0;
		Active = false;
		for (int i = 0; i < Targets.Count; i++)
		{
			if (PlatformTarget == true && Targets[i].GetComponent<MovingPlatformScript>().ReturnToOriginalPosition == true)
			{
				Targets[i].GetComponent<MovingPlatformScript>().Deactivate();
			}
			//if (ChargerTarget == true)
			//{
			//	Targets[i].GetComponent<ChargerScript>().enabled = false;
			//}
		}
	}
}
