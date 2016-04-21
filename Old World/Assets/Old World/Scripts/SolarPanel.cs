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
    private float energy = 0.0f;
    private float t = 0.0f;

    private EmissionIntensityController[] e;

    void Awake()
    {
        //Find every instance of the EmissionIntensityController script on every GameObject
        e = FindObjectsOfType<EmissionIntensityController>();
    }

    protected override void HitByLightStay()
	{
        //energy += Time.deltaTime;

        if(timeIlluminated < ChargeUpTime) Power_bar.GetComponent<Charge_bar>().PowerTurnedOn(ChargeUpTime);

		if (timeIlluminated >= ChargeUpTime)
		{
            Power_bar.GetComponent<Charge_bar>().setColor(Color.white);
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

        //Turn on all lights with the same tag as this generator
        for (int i = 0; i < e.Length; i++)
        {
            //If the Gameobject has the same tag as the generator
            if (e[i].transform.CompareTag(transform.tag))
            {
                e[i].LerpLight(ChargeUpTime);
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
		//Power_bar.GetComponent<Charge_bar>().ChangingColor.g = 0;
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
