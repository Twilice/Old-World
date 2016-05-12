using UnityEngine;
using System.Collections;

public class DoorBug : MonoBehaviour {

    private Light l;
    private float i;
    void Awake()
    {
        l = GetComponent<Light>();
    }

    void Start()
    {
        i = l.intensity;
    }
	
	// Update is called once per frame
	void Update () {
	    if(StateController.roomFullyPowered)
        {
            l.intensity = i;
        }
        else
        {
            l.intensity = 0.3f;
        }
	}
}
