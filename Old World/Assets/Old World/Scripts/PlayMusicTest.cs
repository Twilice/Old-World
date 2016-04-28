using UnityEngine;
using System.Collections;

public class PlayMusicTest : MonoBehaviour {
    
    private EventPlayer player;
	// Use this for initialization
	void Start () {
        player = gameObject.GetComponent<EventPlayer>();
        player.PlayEvent();

    }
    private float test = 0;
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.O))
        {
            player.ChangeParameter("progress", test+=0.25f);
        }
    }
}
