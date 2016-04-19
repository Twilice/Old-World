using UnityEngine;
using System.Collections;

public class EmissiveTest : MonoBehaviour {

    private Renderer r;
    public static float colorIntesity = 0f;
    private Color c;

	// Use this for initialization
	void Start () {
        r = GetComponent<Renderer>();
        c = GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", c*colorIntesity);
        DynamicGI.SetEmissive(r, c * colorIntesity);
	}
}
