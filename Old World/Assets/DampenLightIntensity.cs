using UnityEngine;
using System.Collections;

public class DampenLightIntensity : MonoBehaviour
{
    [Range(0, 1)]
    public float firstPersonViewIntensity = 0.2f;

    private float drainOrGainRate = 1.1f;
    private float originalIntensity;
    private Light light;

    // Use this for initialization
    void Awake()
    {
        light = GetComponent<Light>();
        originalIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(StateController.currentZoom == ZoomStatus.zoomingIn)
        {
            drainIntensity();
        }
        else
        {
            gainIntensity();
        }
    }

    //Drain or Gain light instensity when in first person view
    public void drainIntensity()
    {
        if (light.intensity - Time.deltaTime * drainOrGainRate < firstPersonViewIntensity)
        {
            light.intensity = firstPersonViewIntensity;
        }
        else
        {
            light.intensity -= Time.deltaTime * drainOrGainRate;
        }
    }

    public void gainIntensity()
    {
        if (light.intensity + Time.deltaTime * drainOrGainRate > originalIntensity)
        {
            light.intensity = originalIntensity;
        }
        else
        {
            light.intensity += Time.deltaTime * drainOrGainRate;
        }

    }
}
