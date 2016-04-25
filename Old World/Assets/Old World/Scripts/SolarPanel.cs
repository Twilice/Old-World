using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarPanel : TriggeredByLight
{
    public List<GameObject> Targets;

    public bool platformTarget = false;
    public bool generatorTarget = false;
    public bool chargerTarget = false;

    public float ChargeUpTime;

    //private bool Active = false;
    private float energy = 0.0f;
    private EmissionIntensityController[] e;
    private EmissionIntensityControllerGenerator[] eg;

    void Awake()
    {
        //Find every instance of the EmissionIntensityController and EmissionIntensityControllerGenerator script on every GameObject
        e = FindObjectsOfType<EmissionIntensityController>();
        eg = FindObjectsOfType<EmissionIntensityControllerGenerator>();
    }

    void Update()
    {
        //Drain energy if not hit by light
        if (!isHitByLight)
        {
            drainEnergy();
        }
        UpdateGeneratorLight();
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
        //Drain energy
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

    void UpdateGeneratorLight()
    {
        //Update all lights with the same tag as this generator
        for (int i = 0; i < eg.Length; i++)
        {
            //If the Gameobject has the same tag as the generator
            if (eg[i].transform.CompareTag(transform.tag))
            {
                //Only allow lerp if room is not at max
                if (!RoomState.roomFullyPowered)
                {
                    eg[i].LerpEnergy(energy);
                }
            }
        }
    }
}
