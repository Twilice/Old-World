using UnityEngine;
using System.Collections;

public class MouseHover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}
}
