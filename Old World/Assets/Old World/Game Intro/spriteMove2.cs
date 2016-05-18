using UnityEngine;
using System.Collections;

public class spriteMove2 : MonoBehaviour {

    public Vector3 destination;
    public Vector3 spawn;
    public float speed;
    public int indexToMove;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (IntroScript.instance.currentTextFileID >= indexToMove)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * speed);
        }
        /*if (destination == transform.position)
        {
            print("TP BG");
            transform.position = spawn;
        }
        transform.position = Vector3.Lerp(transform.position, destination, 8);*/
    }
}
