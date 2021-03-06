﻿using UnityEngine;
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
    private ParticleRandomizer[] pr;
    private List<ParticleRandomizer> particleTargets = new List<ParticleRandomizer>();
    private bool activated = false;

    void Awake()
    {
        //Find every instance of the EmissionIntensityController and EmissionIntensityControllerGenerator script on every GameObject
        e = FindObjectsOfType<EmissionIntensityController>();
        eg = FindObjectsOfType<EmissionIntensityControllerGenerator>();
        pr = FindObjectsOfType<ParticleRandomizer>();

        if (pr.Length != 0)
        {
            for (int k = 0; k < pr.Length; k++)
            {
                if (pr[k].transform.parent != null)
                {
                    if (pr[k].transform.parent.CompareTag(tag))
                    {
                        particleTargets.Add(pr[k]);
                    }
                }
                else
                {
                    if (pr[k].CompareTag(tag))
                    {
                        particleTargets.Add(pr[k]);
                    }
                }
            }
        }

        if (generatorTarget)
        {
            GeneratorScript[] gs = FindObjectsOfType<GeneratorScript>();

            foreach (GeneratorScript g in gs)
            {
                if (g.transform.CompareTag(tag))
                {
                    Targets.Add(g.gameObject);
                }
            }
        }
    }

    void Update()
    {
        //Drain energy if not hit by light
        if (!isHitByLight)
        {
            drainEnergy();
        }
        UpdateGeneratorLight();

        if (energy == 1.0f)
        {
            foreach (ParticleRandomizer p in particleTargets)
            {
                p.currentlyCharging = false;
            }
        }

    }

    protected override void HitByLightEnter()
    {

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
                if (!StateController.roomFullyPowered)
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
        OpenAtRoomPower specialDoor;
        if (platformTarget == true && StateController.roomFullyPowered == false && StateController.SegmentActive(tag) == false)
        {
            for (int i = 0; i < Targets.Count; i++)
            {
                specialDoor = Targets[i].GetComponent<OpenAtRoomPower>();
                if (specialDoor != null)
                {
                    specialDoor.Deactivate();
                }
                else
                    foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
                    {
                       movingScript.Deactivate();
                    }
            }
        }

        foreach (ParticleRandomizer p in particleTargets)
        {
            p.currentlyCharging = false;
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
            if (!activated)
            {
                foreach (ParticleRandomizer p in particleTargets)
                {
                    p.currentlyCharging = true;
                }
            }
            energy += Time.deltaTime / ChargeUpTime;
        }

    }

    public void drainEnergy()
    {
        //Drain energy
        if (energy - Time.deltaTime * RoomState.drainAmount * (1f / 0.3f) < 0.0f)
        {
            energy = 0.0f;

            foreach (ParticleRandomizer p in particleTargets)
            {
                p.currentlyDraining = false;
            }
        }
        else
        {
            energy -= Time.deltaTime * RoomState.drainAmount * (1f / 0.3f);

            foreach (ParticleRandomizer p in particleTargets)
            {
                p.currentlyDraining = true;
            }
        }
    }

    void Activate()
    {
        //Play sound once
        //Activate all targets
        GeneratorScript generator;
        ChargerScript charge;
        ParticleBridge bridge;
        OpenAtRoomPower specialDoor;
        for (int i = 0; i < Targets.Count; i++)
        {
            // if (platformTarget == true)
            // {
            if(Targets[i].GetComponent<ComputerInspect>())
            {
                Targets[i].GetComponent<ComputerInspect>().gotExternalPower = true;
            }

            specialDoor = Targets[i].GetComponent<OpenAtRoomPower>();
            if(specialDoor != null)
            {
                specialDoor.Activate();
            }
            else
                foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
                {
                    movingScript.Activate();
                }
            // }
            generator = Targets[i].GetComponent<GeneratorScript>();
            if (generator != null)
            {
                generator.Activate();
            }
            charge = Targets[i].GetComponent<ChargerScript>();
            if (charge != null)
            {
                charge.Activate();
            }
            bridge = Targets[i].GetComponent<ParticleBridge>();
            if(bridge != null)
            {
                bridge.Activate();
            }
        }

        activated = true;
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
                if (!StateController.roomFullyPowered)
                {
                    eg[i].LerpEnergy(energy);
                }
                else
                    eg[i].LerpEnergy(1);
            }
        }
    }
}
