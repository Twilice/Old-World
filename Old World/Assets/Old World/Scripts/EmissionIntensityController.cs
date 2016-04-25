using UnityEngine;
using System.Collections;

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

    void Awake()
    {
        r = GetComponent<Renderer>();
        mr = GetComponent<MeshRenderer>();
        c = mr.material.GetColor("_EmissionColor");
        ehbl = GetComponent<EmissiveHitByLight>();

        generatorActiveEnergy = generatorActiveEnergyPercentage / 100f;
        roomActiveEnergy = roomActiveEnergyPercentage / 100f;
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
        if (RoomState.roomFullyPowered)
        {
            LerpEnergy();
        }

        if(emissionIntensity < 0.8f)
        {
            mr.material.SetColor("_EmissionColor", c * emissionIntensity / 2f);
        }
        
        DynamicGI.SetEmissive(r, c * emissionIntensity);
        //Debug.Log(emissionIntensity + ", " + energy);
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
        if(energy + Time.deltaTime * RoomState.gainAmount > 1)
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
        if (roomActivated)
        {
            if(energy - Time.deltaTime * RoomState.drainAmount < roomActiveEnergy)
            {
                energy = roomActiveEnergy;
            }
            else
            {
                energy -= Time.deltaTime * RoomState.drainAmount;
            }
        }
        else if(generatorActivated)
        {
            if (energy - Time.deltaTime * RoomState.drainAmount < generatorActiveEnergy)
            {
                energy = generatorActiveEnergy;
            }
            else
            {
                energy -= Time.deltaTime * RoomState.drainAmount;
            }
        }
        else
        {
            if (energy - Time.deltaTime * RoomState.drainAmount < 0.0f)
            {
                energy = 0.0f;
            }
            else
            {
                energy -= Time.deltaTime * RoomState.drainAmount;
            }
        }
    }
}
