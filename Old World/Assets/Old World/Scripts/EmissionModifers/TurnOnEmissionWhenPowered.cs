using UnityEngine;
using System.Collections;

public class TurnOnEmissionWhenPowered : MonoBehaviour {

    private Renderer rend;
    // Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_EmissionColor", Color.black);
    }
	
	// Update is called once per frame
	void Update () {
        // todo make an actionlistener instead of if spin
        if (RoomState.roomFullyPowered)
        {
            rend.material.SetColor("_EmissionColor", Color.white);
        }
    }
}
