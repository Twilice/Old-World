using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class RoomState : MonoBehaviour {

    public static bool roomFullyPowered = false;
    public static float drainAmount = 0.1f;
    public static float gainAmount = 0.3f;

    private GeneratorScript[] generators;
    [Header("Music")]
    public string eventName = "event:/Wing 1.1 (Test 2)";
    public string parameterName = "progress";
    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance musicEvent;
    FMOD.Studio.ParameterInstance musicParameter;
    private float musicParamValue = 0;

    //Initiate generator array
    void Awake()
    {
        generators = FindObjectsOfType<GeneratorScript>();
        musicEvent = FMODUnity.RuntimeManager.CreateInstance(eventName);
        musicEvent.getParameter(parameterName, out musicParameter);
        InvokeRepeating("UpdateGenerators", 0, 0.1f);
        musicParameter.setValue(musicParamValue);
       // musicEvent.start();
    }

	// Update is called once per frame
	void UpdateGenerators() {

        //Set roomFullyPowered to correct value
        bool isPowerered = true;
        musicParamValue = 0f;
        for (int i = 0; i < generators.Length; i++)
        {
            if (generators[i].Active == false)
            {
                isPowerered = false;
            }
            else musicParamValue+=0.25f;
        }
        musicParameter.setValue(musicParamValue);
        roomFullyPowered = isPowerered;
    }
}
