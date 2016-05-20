using UnityEngine;
using System.Collections;

public class ParticleRandomizer : MonoBehaviour
{
    public bool whenever = true;
    public bool whenCharging = false;
    public bool whenDraining = false;
    public bool whenRoomActive = false;
    public bool beforeRoomActive = false;
    public bool randomBurstTime = true;
    public float randomBurstTimeOffset = 0.2f;
    public float randomInitialTime = 4.0f;

    //RANDOM START TIME OFFSET

    public float burstTime = 0.5f;
    public float approximateIntervals = 6.0f;
    public float randomOffsetMax = 3.0f;

    [HideInInspector]
    public bool currentlyCharging = false;
    [HideInInspector]
    public bool currentlyDraining = false;
    private bool oneTime = true;
    private bool newRand = true;
    private bool firstTimeNewRand = true;
    private float offset;
    private float burstOffset;
    private float firstTimeOffset;
    private float timer = 0.0f;

    private ParticleSystem particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        firstTimeOffset = Random.Range(0, randomInitialTime);
    }

    void Update()
    {
        //Increase the timer
        timer += Time.deltaTime;

        //Generate new random offset
        if (newRand)
        {
            offset = Random.Range(-randomOffsetMax, randomOffsetMax);
            burstOffset = Random.Range(-randomBurstTimeOffset, randomBurstTimeOffset);
            newRand = false;

            if(firstTimeNewRand)
            {
                firstTimeNewRand = false;
            }
            else
            {
                firstTimeOffset = 0;
            }
        }

        if (whenever)
        {

            if (timer >= approximateIntervals + offset + burstTime + burstOffset + firstTimeOffset) //If the burst should end
            {
                timer = 0.0f;
                newRand = true;
                oneTime = true;
                particle.Stop(true);
            }
            else if (timer >= approximateIntervals + offset + firstTimeOffset) //When the burst should start
            {
                if (oneTime)
                {
                    oneTime = false;
                    particle.Play(true);
                }
            }
        }
        else if(beforeRoomActive && !StateController.roomFullyPowered)
        {
            if (timer >= approximateIntervals + offset + burstTime + burstOffset + firstTimeOffset) //If the burst should end
            {
                timer = 0.0f;
                newRand = true;
                oneTime = true;
                particle.Stop(true);
            }
            else if (timer >= approximateIntervals + offset + firstTimeOffset) //When the burst should start
            {
                if (oneTime)
                {
                    oneTime = false;
                    particle.Play(true);
                }
            }
        }
        else if (whenCharging)
        {
            if (timer >= approximateIntervals + offset + burstTime + burstOffset + firstTimeOffset) //If the burst should end
            {
                timer = 0.0f;
                newRand = true;
                oneTime = true;
                particle.Stop(true);
            }
            else if (timer >= approximateIntervals + offset + firstTimeOffset && currentlyCharging) //When the burst should start
            {
                if (oneTime)
                {
                    oneTime = false;
                    particle.Play(true);
                }
            }
        }
        else if(whenDraining)
        {
            if (timer >= approximateIntervals + offset + burstTime + burstOffset + firstTimeOffset) //If the burst should end
            {
                timer = 0.0f;
                newRand = true;
                oneTime = true;
                particle.Stop(true);
            }
            else if (timer >= approximateIntervals + offset + firstTimeOffset && currentlyDraining) //When the burst should start
            {
                if (oneTime)
                {
                    oneTime = false;
                    particle.Play(true);
                }
            }
        }
        else
        {
            particle.Stop(true);
        }
    }
}

