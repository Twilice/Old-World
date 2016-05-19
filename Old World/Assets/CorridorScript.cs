using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CorridorScript : MonoBehaviour {

	void Start () {
        StateController.LoadGame(Rooms.Hub);

    }
	
}
