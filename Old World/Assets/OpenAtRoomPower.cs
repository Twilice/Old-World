using UnityEngine;
using System.Collections;

public class SolarDoorScript : MonoBehaviour {

    private MovingPlatformScript[] mps;
    // Use this for initialization
    void Start () {
        mps = GetComponentsInChildren<MovingPlatformScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StateController.roomFullyPowered)
        {
            foreach (MovingPlatformScript movingScript in mps)
            {
                movingScript.Activate();
            }
        }
    }
}
