using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Characters.FirstPerson;

public class FirstPersonViewToggle : MonoBehaviour {
    private MouseOrbitImproved moi;
    private ThirdPersonCharacter tpc;
    private ThirdPersonUserControl tpuc;
    private RigidbodyFirstPersonController rbfpc;
    // Use this for initialization
    void Start () {
        
        moi = GetComponent<MouseOrbitImproved>();
        moi.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(1))
        {
            moi.enabled = false;
            
        }
	}
}
