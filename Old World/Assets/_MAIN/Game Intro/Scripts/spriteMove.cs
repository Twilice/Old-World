using UnityEngine;
using System.Collections;

public class spriteMove : MonoBehaviour {

    public Vector3 newPos;
 //   private Vector3 oriPos;
    public int moveAtIndex;
    public float speed;

	// Use this for initialization
	void Start () {
    //    oriPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    if (IntroScript.instance.currentTextFileID >= moveAtIndex)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
        }
	}
}
