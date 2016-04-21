using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(EmissiveHitByLight))]
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
    [Range(0, 100)]
    public float drainPercentagePerSecond = 10.0f;
    [Range(0, 100)]
    public float gainPercentagePerSecond = 20.0f;

    private EmissiveHitByLight ehbl;
    private float t = 0.0f;
    private float t1 = 0.0f;
    private Renderer r;
    private float emissionIntensity = 0f;
    private Color c;
    private MeshRenderer mr;
    private bool firstTime = true;
    private bool firstTime1 = true;
    private float fromEnergy;
    private float energy = 0.0f;
    private float generatorActiveEnergy;
    private float roomActiveEnergy;
    private float gainAmount;
    private float drainAmount;
    private bool generatorActivated = false;
    private float lastTime = 0.0f;

    void Awake()
    {
        r = GetComponent<Renderer>();
        mr = GetComponent<MeshRenderer>();
        c = mr.material.GetColor("_EmissionColor");
        ehbl = GetComponent<EmissiveHitByLight>();

        generatorActiveEnergy = generatorActiveEnergyPercentage / 100f;
        roomActiveEnergy = roomActiveEnergyPercentage / 100f;
        gainAmount = gainPercentagePerSecond / 100f;
        drainAmount = drainPercentagePerSecond / 100f;
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

        mr.material.SetColor("_EmissionColor", c * emissionIntensity / 2f);
        DynamicGI.SetEmissive(r, c * emissionIntensity);
    }

    public void SetEmissionIntesity(float f)
    {
        emissionIntensity = f;
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
            t1 += Time.deltaTime * (1 / lerpTimeWhenRoomFullyPowered);
            energy = Mathf.Lerp(fromEnergy, roomActiveEnergy, t1);
        }
    }

    //When generator is activated
    public void LerpEnergy(float lerpTime)
    {
        if (firstTime)
        {
            fromEnergy = energy;
            firstTime = false;
        }

        if (Time.time - lastTime > 0.1f)
        {
            t = 0.0f;
        }

        float factor = 1 / (lerpTime);
        if (t < 1.0f)
        {
            t += Time.deltaTime * factor;
            energy = Mathf.Lerp(fromEnergy, generatorActiveEnergy, t);
        }
        else
        {
            generatorActivated = true;
        }

        lastTime = Time.time;
    }

    public void gainEnergy()
    {
        if(energy + Time.deltaTime * gainAmount > 1)
        {
            energy = 1.0f;
        }
        else
        {
            energy += Time.deltaTime * gainAmount;
        }
        
    }

    public void drainEnergy()
    {
        if (RoomState.roomFullyPowered)
        {
            if(energy - Time.deltaTime * drainAmount < roomActiveEnergy)
            {
                energy = roomActiveEnergy;
            }
            else
            {
                energy -= Time.deltaTime * drainAmount;
            }
        }
        else if(generatorActivated)
        {
            if (energy - Time.deltaTime * drainAmount < generatorActiveEnergy)
            {
                energy = generatorActiveEnergy;
            }
            else
            {
                energy -= Time.deltaTime * drainAmount;
            }
        }
        else
        {
            if (energy - Time.deltaTime * drainAmount < 0.0f)
            {
                energy = 0.0f;
            }
            else
            {
                energy -= Time.deltaTime * drainAmount;
            }
        }
    }
}
