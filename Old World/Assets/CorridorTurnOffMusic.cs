using UnityEngine;
using System.Collections;

public class CorridorTurnOffMusic : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag.Equals("Player"))
            StateController.EnterCorridor();
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag.Equals("Player"))
            StateController.LeaveCorridor();
    }
}
