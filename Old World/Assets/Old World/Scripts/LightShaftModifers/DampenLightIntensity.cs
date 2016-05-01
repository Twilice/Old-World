using UnityEngine;
using System.Collections;

public class DampenLightIntensity : MonoBehaviour
{
    [Range(0, 1)]
    public float firstPersonViewIntensity = 0.2f;

    private float drainOrGainRate = 1.5f;
    private float originalIntensity;
    private LightShafts dampedLight;
    private LensReflect lr;
    private bool overriddenWhenFullyPowered = false;

    // Use this for initialization
    void Awake()
    {
        DampenLightWhenRoomFullyPowered d = GetComponent<DampenLightWhenRoomFullyPowered>();
        if (d != null)
        {
            overriddenWhenFullyPowered = true;
        }
        dampedLight = GetComponent<LightShafts>();
        lr = FindObjectOfType<LensReflect>();
        originalIntensity = dampedLight.m_Brightness;
    }

    // Update is called once per frame
    void Update()
    {
        if (!overriddenWhenFullyPowered)
        {
            if (StateController.currentZoom == ZoomStatus.zoomingIn && lr.inLight)
            {
                drainIntensity();
            }
            else
            {
                gainIntensity();
            }
        }
        else
        {
            if (!RoomState.roomFullyPowered)
            {
                if (StateController.currentZoom == ZoomStatus.zoomingIn && lr.inLight)
                {
                    drainIntensity();
                }
                else
                {
                    gainIntensity();
                }
            }
        }
    }

    //Drain or Gain light instensity when in first person view
    public void drainIntensity()
    {
        if (dampedLight.m_Brightness - Time.deltaTime * drainOrGainRate < firstPersonViewIntensity)
        {
            dampedLight.m_Brightness = firstPersonViewIntensity;
        }
        else
        {
            dampedLight.m_Brightness -= Time.deltaTime * drainOrGainRate;
        }
    }

    public void gainIntensity()
    {
        if (dampedLight.m_Brightness + Time.deltaTime * drainOrGainRate > originalIntensity)
        {
            dampedLight.m_Brightness = originalIntensity;
        }
        else
        {
            dampedLight.m_Brightness += Time.deltaTime * drainOrGainRate;
        }

    }
}
