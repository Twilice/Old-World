using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class GeneratorScript : MonoBehaviour
{
    public List<GameObject> Targets;
    public bool PlatformTarget;
    public bool GeneratorTarget;
    public bool ChargerTarget;
    public bool Active = false;

    private DoorLight doorLight;
    private DoorLight[] allDoorLights;
    private MovingPlatformScript[] mps;
    private List<MovingPlatformScript> targetPlatforms = new List<MovingPlatformScript>();
    private ChargerScript[] cs;
    private List<ChargerScript> targetChargers = new List<ChargerScript>();

    void Awake()
    {
        allDoorLights = FindObjectsOfType<DoorLight>();
        mps = FindObjectsOfType<MovingPlatformScript>();
        cs = FindObjectsOfType<ChargerScript>();

        //The doorLight with the same tag
        for (int i = 0; i < allDoorLights.Length; i++)
        {
            if (allDoorLights[i].transform.CompareTag(transform.tag))
            {
                doorLight = allDoorLights[i];
            }
        }

        //The movingPlatforms with the same tag
        for (int i = 0; i < mps.Length; i++)
        {
            if (mps[i].transform.CompareTag(transform.tag))
            {
                targetPlatforms.Add(mps[i]);
            }
        }

        //The chargerScripts with the same tag
        for (int i = 0; i < cs.Length; i++)
        {
            if (cs[i].transform.CompareTag(transform.tag))
            {
                targetChargers.Add(cs[i]);
            }
        }
    }

    void Start()
    {
        if(StateController.SegmentActive(tag))
        {
            Active = true;
            if (doorLight != null)
                doorLight.Activate();
        }
    }

    void Update()
    {
        if (Active == true)
        {
            if (PlatformTarget)
            {
                //Activate the platforms
                for (int i = 0; i < targetPlatforms.Count; i++)
                {
                    targetPlatforms[i].Activate();
                    targetPlatforms[i].returning = false;
                }
            }

            if (ChargerTarget)
            {
                //Activate the chargers
                for (int i = 0; i < targetChargers.Count; i++)
                {
                    targetChargers[i].Activate();
                }
            }
        }
    }

    public void Activate()
    {
        StateController.ActivateSegment(tag);

        //Change bool to true
        Active = true;

        //Change the light
        if(doorLight != null)
            doorLight.Activate();
    }
}
