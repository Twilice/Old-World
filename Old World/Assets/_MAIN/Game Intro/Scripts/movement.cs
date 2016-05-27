using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class movement : MonoBehaviour {
    public List<Vector3> positions = new List<Vector3>();
    public int posIndex = 1;
    private Vector3 oriPos;

    public float speed;
    private float actualSpeed;
    private float c;
	// Use this for initialization
	void Start () {
        oriPos = positions[0];
        transform.position = oriPos;
	}
	
	// Update is called once per frame
	void Update () {
        actualSpeed = Mathf.Lerp(actualSpeed, speed, Time.deltaTime * 5);
        transform.position = Vector3.Lerp(transform.position, positions[posIndex], Time.deltaTime * actualSpeed);
	}
    public void cameraMovement()
    {
        posIndex++;
    }
}
