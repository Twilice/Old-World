using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class ColliderDoorScript : MonoBehaviour
{
    //private float t = 0.0f;
    private MovingPlatformScript[] mps;
    [Header("If active without power")]
    public bool active = false;
    private bool gotPower = false;
    public static FMOD.Studio.EventInstance doorOpen;
    public static FMOD.Studio.EventInstance doorClose;

    void Awake()
    {
        mps = GetComponentsInChildren<MovingPlatformScript>();
        doorOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Door/Door_Open");
        doorClose = FMODUnity.RuntimeManager.CreateInstance("event:/Door/Door_Close");
        if (active)
            gotPower = true;
    }

    void Update()
    {
        doorOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        doorClose.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        if (StateController.roomFullyPowered || StateController.SegmentActive(tag) || active)
        {
            gotPower = true;
            //todo door won't open if you are standing in trigger when it turns online, check if isintrigger
        }
        else
            gotPower = false;


    }

    void Activate()
    {
		foreach (MovingPlatformScript movingScript in mps)
		{
			movingScript.Activate();
		}
	}

    void OnTriggerEnter()
    {
        if (gotPower)
            if(mps.Length > 0)
            {
                doorOpen.start();
                foreach (MovingPlatformScript movingScript in mps)
                {
                    movingScript.Activate();
                }
            }
    }

   /* void OnTriggerStay()
    {
        if(active)
            foreach (MovingPlatformScript movingScript in mps)
            {
                movingScript.returning = false;
                Activate();
            }
    }*/

    void OnTriggerExit()
    {
        if (gotPower)
            if (mps.Length > 0)
            {
                doorClose.start();
                foreach (MovingPlatformScript movingScript in mps)
                {
                    movingScript.Deactivate();
                }
            }
    }
}
