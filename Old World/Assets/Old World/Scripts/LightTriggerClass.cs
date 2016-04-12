using UnityEngine;
using System.Collections;

public abstract class TriggeredByLight : MonoBehaviour {

	/*
		TODO : implement multiple light on same target leaving/exiting correctly 

	*/

	public bool isHitByLight{get; private set;}
	public float timeIlluminated { get; private set; }

    virtual protected void HitByLightEnter(){}

    virtual protected void HitByLightExit(){}

    virtual protected void HitByLightStay(){}

	public void CallHitByLightEnter()
	{
		timeIlluminated = 0f; 
		isHitByLight = true;
		HitByLightEnter();
	}

	public void CallHitByLightExit()
	{
		timeIlluminated = 0f;
		isHitByLight = false;
		HitByLightExit();
	}

	public void CallHitByLightStay()
	{
		timeIlluminated += Time.deltaTime;
		HitByLightStay();
	}
}