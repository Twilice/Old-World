using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarPanel : TriggeredByLight
{
	public List<GameObject> Targets;

	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;

	public float ChargeUpTime;

	//private bool Active = false;
    private float energy = 0.0f;

    private EmissionIntensityController[] e;

    void Awake()
    {
        //Find every instance of the EmissionIntensityController script on every GameObject
        e = FindObjectsOfType<EmissionIntensityController>();
    }

    void Update()
    {
        //Drain energy if not hit by light
        if (!isHitByLight)
        {
            drainEnergy();
        }
    }
    protected override void HitByLightStay()
	{
        //Gain energy when hit by light
        gainEnergy();

        //Increase intensity on all lights with the same tag as this generator
        for (int i = 0; i < e.Length; i++)
		{
            //If the Gameobject has the same tag as the generator
            if (e[i].transform.CompareTag(transform.tag))
            {
                //Only allow lerp if room is not at max
                if (!RoomState.roomFullyPowered)
                {
                    e[i].LerpEnergy(energy);
                }
            }
        }

        //Play powering up sound
        //Fade lights on?

    }

    protected override void HitByLightExit()
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
		}
    }

    public void gainEnergy()
    {
        //Fully charged
        if (energy + Time.deltaTime / ChargeUpTime > 1)
				{
            energy = 1.0f;
            Activate();
        }//Keep charging
        else
				{
            energy += Time.deltaTime / ChargeUpTime;
			}

		}

    public void drainEnergy()
        {
        if (energy - Time.deltaTime * RoomState.drainAmount * (1f / 0.3f) < 0.0f)
            {
            energy = 0.0f;
            }
        else
        {
            energy -= Time.deltaTime * RoomState.drainAmount * (1f / 0.3f);
        }
    }

    void Activate()
	{
        //Play sound once

        //Activate all targets
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
            if (ChargerTarget == true)
			{
                Targets[i].GetComponent<MovingPlatformScript>().returning = true;
			}
		}
	}
}
