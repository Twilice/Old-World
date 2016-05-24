using UnityEngine;
using System.Collections;

public class lensBeamSound : MonoBehaviour {

    FMOD.Studio.EventInstance soundLensBeam;
    FMOD.Studio.ParameterInstance paramInstance;

    bool hasBarked = false;
    FMOD.Studio.EventInstance barkToPlay;

    void Awake()
    {
        soundLensBeam = FMODUnity.RuntimeManager.CreateInstance("event:/Test/Beam");
        soundLensBeam.getParameter("Beam", out paramInstance);
        barkToPlay = FMODUnity.RuntimeManager.CreateInstance("event:/Barks/Entering_light_spot");
    }

    void OnEnable()
    {
        if(hasBarked == false)
        {
            hasBarked = true;
            barkToPlay.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            barkToPlay.start();
        }

        paramInstance.setValue(0);
        soundLensBeam.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        soundLensBeam.start();
    }

    void OnDisable()
    {
        paramInstance.setValue(1);
    }
}
