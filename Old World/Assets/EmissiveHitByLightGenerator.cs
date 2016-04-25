using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EmissionIntensityControllerGenerator))]
public class EmissiveHitByLightGenerator : TriggeredByLight
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
