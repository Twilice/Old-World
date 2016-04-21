using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EmissionIntensityController))]
public class EmissiveHitByLight : TriggeredByLight
{
    private EmissionIntensityController ec;
    void Awake()
    {
        ec = GetComponent<EmissionIntensityController>();
    }


    protected override void HitByLightStay()
    {
        ec.gainEnergy();
    }
}
