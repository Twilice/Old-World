using UnityEngine;
using System.Collections;

public class lensBeamSound : MonoBehaviour {

    FMOD.Studio.EventInstance soundLensBeam;
    FMOD.Studio.ParameterInstance paramInstance;
    void Awake()
    {
        soundLensBeam = FMODUnity.RuntimeManager.CreateInstance("event:/Test/Beam");
        soundLensBeam.getParameter("Beam", out paramInstance);
    }

    void OnEnable()
    {
        paramInstance.setValue(0);
        soundLensBeam.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        soundLensBeam.start();
    }

    void OnDisable()
    {
        paramInstance.setValue(1);
    }
}
