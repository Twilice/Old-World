using UnityEngine;
using System.Collections;

public class OpenAtRoomPower : MonoBehaviour {

    private MovingPlatformScript[] mps;
    public static FMOD.Studio.EventInstance doorOpen;
    public static FMOD.Studio.EventInstance doorClose;
    // Use this for initialization
    void Start () {
        mps = GetComponentsInChildren<MovingPlatformScript>();
        doorOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Door/Door_Open");
        doorClose = FMODUnity.RuntimeManager.CreateInstance("event:/Door/Door_Close");
    }

    // Update is called once per frame
    void Update()
    {
        doorOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        doorClose.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        if (StateController.roomFullyPowered)
        {
            Activate();
        }
    }
    private bool activated = false;
    public void Activate()
    {
        if (activated == false)
        {
            activated = true;
            doorOpen.start();
            foreach (MovingPlatformScript movingScript in mps)
            {
                movingScript.Activate();
            }
        }
    }

    public void Deactivate()
    {
        if (activated == true)
        {
            activated = false;
            doorClose.start();
            foreach (MovingPlatformScript movingScript in mps)
            {
                movingScript.Deactivate();
            }
        }
    }
}
