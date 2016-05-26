using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class RoomState : MonoBehaviour {
    
    public static float drainAmount = 0.1f;
    public static float gainAmount = 0.3f;
    public Rooms room;
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
    void Start()
    {
        StateController.currentRoom = room;
        StateController.TurnOnMusic();
        generators = FindObjectsOfType<GeneratorScript>();
    }

    void Update()
    {
        UpdateGenerators();
    }

	// Update is called once per frame
	void UpdateGenerators() {
        if (StateController.loading == false && room.Equals(StateController.currentRoom) == true)
        {
            //Set roomFullyPowered to correct value
            bool isPowerered = StateController.currentRoom != Rooms.Hub;
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
            if (StateController.loading == false || room.Equals(StateController.currentRoom) == false) //fuck it I'm tired
            { StateController.roomFullyPowered = isPowerered; }
        }
    }
}
