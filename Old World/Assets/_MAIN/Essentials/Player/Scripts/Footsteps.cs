using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

    public static FMOD.Studio.EventInstance footStep;
    public static FMOD.Studio.ParameterInstance paramInstance;

    // Use this for initialization
    void Awake () {
        footStep = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Footsteps");
        footStep.getParameter("Floor", out paramInstance);
        paramInstance.setValue(0.21f);
    }
    // Update is called once per frame
    void Update () {
        footStep.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    void Footstep(AnimationEvent evt)
    {
        if (evt.animatorClipInfo.weight > 0.5)
            footStep.start();
    }
}
