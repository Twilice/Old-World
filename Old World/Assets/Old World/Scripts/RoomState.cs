using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class RoomState : MonoBehaviour {

    public static bool roomFullyPowered = false;

    private GeneratorScript[] generators;

    //Initiate generator array
    void Awake()
    {
        generators = FindObjectsOfType<GeneratorScript>();
    }

	// Update is called once per frame
	void Update () {

        //Set roomFullyPowered to correct value
        bool isPowerered = true;
        for (int i = 0; i < generators.Length; i++)
        {
            if (generators[i].Active == false)
            {
                isPowerered = false;
            }
        }
        roomFullyPowered = isPowerered;
    }
}
