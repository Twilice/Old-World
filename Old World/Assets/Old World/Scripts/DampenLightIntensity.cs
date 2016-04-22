using UnityEngine;
using System.Collections;

public class DampenLightIntensity : MonoBehaviour
{
    [Range(0, 1)]
    public float firstPersonViewIntensity = 0.2f;

    private float drainOrGainRate = 1.1f;
    private float originalIntensity;
    private Light dampedLight;

    // Use this for initialization
    void Awake()
    {
        dampedLight = GetComponent<Light>();
        originalIntensity = dampedLight.intensity;
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
        if (dampedLight.intensity - Time.deltaTime * drainOrGainRate < firstPersonViewIntensity)
        {
            dampedLight.intensity = firstPersonViewIntensity;
        }
        else
        {
            dampedLight.intensity -= Time.deltaTime * drainOrGainRate;
        }
    }

    public void gainIntensity()
    {
        if (dampedLight.intensity + Time.deltaTime * drainOrGainRate > originalIntensity)
        {
            dampedLight.intensity = originalIntensity;
        }
        else
        {
            dampedLight.intensity += Time.deltaTime * drainOrGainRate;
        }

    }
}
