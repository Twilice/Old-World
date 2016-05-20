using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class RoomState : MonoBehaviour {
    
    public static float drainAmount = 0.1f;
    public static float gainAmount = 0.3f;
    private GeneratorScript[] generators;
    /* hårdkådat i statecontroller för stunden, funkar om det skulle bara vara ett musikspår

    [Header("Music")]
    public string eventName = "event:/Wing 1.1 (Test 2)";
    public string parameterName = "progress";
    public float parameterIncrement = 0.25f;
    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance musicEvent;
    FMOD.Studio.ParameterInstance musicParameter;
    private float musicParamValue = 0; */

    //Initiate generator array
    void Awake()
    {
        generators = FindObjectsOfType<GeneratorScript>();
       InvokeRepeating("UpdateGenerators", 0, 0.1f);
       
    
    }

	// Update is called once per frame
	void UpdateGenerators() {

        //Set roomFullyPowered to correct value
        bool isPowerered = true;
     //   StateController.musicParamValue = 0f;
        for (int i = 0; i < generators.Length; i++)
        {
            if (generators[i].Active == false)
            {
                isPowerered = false;
            }
          //  else StateController.musicParamValue += StateController.parameterIncrement;
        }
       // StateController.musicParameter.setValue(StateController.musicParamValue);
        StateController.roomFullyPowered = isPowerered;
    }
}
