using UnityEngine;
using System.Collections;

public class CorridorTurnOffMusic : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag.Equals("Player"))
            StateController.TurnOffMusic();
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag.Equals("Player"))
            StateController.TurnOnMusic();
    }
}
