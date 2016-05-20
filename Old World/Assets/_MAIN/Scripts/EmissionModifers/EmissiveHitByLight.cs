using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(EmissionIntensityController))]
public class EmissiveHitByLight : TriggeredByLight
{
    private ParticleRandomizer[] particleTargets;

    private EmissionIntensityController ec;
    void Awake()
    {
        ec = GetComponent<EmissionIntensityController>();

        particleTargets = GetComponentsInChildren<ParticleRandomizer>();
    }
    protected override void HitByLightEnter()
    {
        foreach (ParticleRandomizer p in particleTargets)
        {
            p.currentlyCharging = true;
        }
    }

    protected override void HitByLightExit()
    {
        foreach (ParticleRandomizer p in particleTargets)
        {
            p.currentlyCharging = false;
        }
    }

    protected override void HitByLightStay()
    {
        ec.gainEnergy();
    }
}
