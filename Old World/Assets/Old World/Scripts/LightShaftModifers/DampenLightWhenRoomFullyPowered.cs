using UnityEngine;
using System.Collections;

public class DampenLightWhenRoomFullyPowered : MonoBehaviour
{
    private float firstPersonViewIntensity = 0.15f;

    private float drainOrGainRate = 0.7f;
    private LightShafts dampedLight;

    // Use this for initialization
    void Awake()
    {
        dampedLight = GetComponent<LightShafts>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StateController.roomFullyPowered)
        {
            drainIntensity();
        }
    }

    //Drain or Gain light instensity when in first person view
    public void drainIntensity()
    {
        if (dampedLight.m_Brightness - Time.deltaTime * drainOrGainRate < firstPersonViewIntensity)
        {
            dampedLight.m_Brightness = firstPersonViewIntensity;
            enabled = false;
        }
        else
        {
            dampedLight.m_Brightness -= Time.deltaTime * drainOrGainRate;
        }
    }
}
