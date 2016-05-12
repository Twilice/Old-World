using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class EmissionIntensityController : MonoBehaviour
{

    [Range(0, 5)]
    public float maxIntensity = 2.7f;
    [Range(0, 100)]
    public float roomActiveEnergyPercentage = 100;
    [Range(0, 100)]
    public float generatorActiveEnergyPercentage = 30;
    [Range(0, 20)]
    public float lerpTimeWhenRoomFullyPowered = 4.0f;

    private EmissiveHitByLight ehbl;
    private SolarPanel solarEhbl;
    private SolarPanel[] solarPanels;
    private float t1 = 0.0f;
    private Renderer r;
    private float emissionIntensity = 0f;
    private Color c;
    private MeshRenderer mr;
    private bool firstTime1 = true;
    private float fromEnergy;
    private float energy = 0.0f;
    private float generatorActiveEnergy;
    private float roomActiveEnergy;
    private bool generatorActivated = false;
    private bool roomActivated = false;
    private bool energyThreshold = true;
    private bool isConnectedToSolarPanel = false;
    private float offset = 0.0f;
    private float flickerDuration = 0.0f;

    void Awake()
    {
        r = GetComponent<Renderer>();
        mr = GetComponent<MeshRenderer>();
        c = mr.material.GetColor("_EmissionColor");
        ehbl = GetComponent<EmissiveHitByLight>();
        solarPanels = FindObjectsOfType<SolarPanel>();

        for (int i = 0; i < solarPanels.Length; i++)
        {
            //If this is this light's solar panel
            if (solarPanels[i].transform.CompareTag(transform.tag))
            {
                solarEhbl = solarPanels[i];
                isConnectedToSolarPanel = true;
            }
        }

        generatorActiveEnergy = generatorActiveEnergyPercentage / 100f;
        roomActiveEnergy = roomActiveEnergyPercentage / 100f;
    }

    void Start()
    {
        if(StateController.roomFullyPowered)
        {
            energy = roomActiveEnergy;
            generatorActivated = true;
        }
        else if(StateController.isSegmentActive(tag))
        {
            generatorActivated = true;
            energy = generatorActiveEnergy;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Drain light if it's not hit by light
        if (!ehbl.isHitByLight)
        {
            drainEnergy();
        }

        emissionIntensity = maxIntensity * energy;

        //Lerp the energy to roomActiveEnergy when the room is fully powered
        if (StateController.roomFullyPowered)
        {
            if (energy < roomActiveEnergy)
                LerpEnergy();
        }

        //To prevent the color from being too bright
        if (emissionIntensity < 0.8f)
        {
            mr.material.SetColor("_EmissionColor", c * emissionIntensity);
        }

        DynamicGI.SetEmissive(r, c * emissionIntensity);

    }

    //When the room is fully powered
    void LerpEnergy()
    {
        if (firstTime1)
        {
            fromEnergy = energy;
            firstTime1 = false;
        }

        if (t1 < 1.0f)
        {
            t1 += Time.deltaTime / lerpTimeWhenRoomFullyPowered;
            energy = Mathf.Lerp(fromEnergy, roomActiveEnergy, t1);
        }
        else
            roomActivated = true;
    }

    //When the solarpanel is hit
    public void LerpEnergy(float solarPanelEnergy)
    {
        if (solarPanelEnergy < 1.0f)
        {
            float tmpenergy = Mathf.Lerp(0, generatorActiveEnergy, solarPanelEnergy); //Is fromEnergy really right here?
            if (energy < tmpenergy)
                energy = tmpenergy;
        }
        else
        {
            generatorActivated = true;
        }
    }

    public void gainEnergy()
    {
        if (energy + Time.deltaTime * RoomState.gainAmount > 1)
        {
            energy = 1.0f;
        }
        else
        {
            energy += Time.deltaTime * RoomState.gainAmount;
        }

    }

    public void drainEnergy()
    {
        //Flicker lights if they are currently draining
        if (!StateController.roomFullyPowered)
        {
            if (isConnectedToSolarPanel)
            {
                if (!energyThreshold && !solarEhbl.isHitByLight)
                    FlickerLight();
            }
            else
            {
                if (!energyThreshold)
                    FlickerLight();
            }
        }


        //Drain Energy
        if (roomActivated)
        {
            if (energy - Time.deltaTime * RoomState.drainAmount < roomActiveEnergy)
            {
                energy = roomActiveEnergy;
                energyThreshold = true;
            }
            else
            {
                energy -= Time.deltaTime * RoomState.drainAmount;
                energyThreshold = false;
            }
        }
        else if (generatorActivated)
        {
            if (energy - Time.deltaTime * RoomState.drainAmount < generatorActiveEnergy)
            {
                energy = generatorActiveEnergy;
                energyThreshold = true;
            }
            else
            {
                energy -= Time.deltaTime * RoomState.drainAmount;
                energyThreshold = false;
            }
        }
        else
        {
            if (energy - Time.deltaTime * RoomState.drainAmount < 0.0f)
            {
                energy = 0.0f;
                energyThreshold = true;
            }
            else
            {
                energy -= Time.deltaTime * RoomState.drainAmount;
                energyThreshold = false;
            }
        }
    }

    void FlickerLight()
    {
        //Flicker light
        if (energy > 0)
        {
            //No flicker in progress
            if (flickerDuration == 0)
            {
                float rand = Random.value;

                //Maybe start new flicker
                if (rand < 0.015f)
                {
                    FlickerOn();
                    flickerDuration = Random.value / 2f;
                }
            }
            else //Flicker in progress
            {
                //Reduce flickerDuration
                if (flickerDuration - Time.deltaTime < 0)
                {
                    flickerDuration = 0;
                    FlickerOff();
                }
                else
                    flickerDuration -= Time.deltaTime;
            }
        }
    }
    public void FlickerOn()
    {
        offset = Random.value / 4f;
        energy -= offset;
    }

    public void FlickerOff()
    {
        energy += offset;
    }
}
