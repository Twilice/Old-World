using UnityEngine;
using System.Collections;

public abstract class TriggeredByLight : MonoBehaviour {

	public bool isHitByLight{get; private set;}
	public float timeIlluminated { get; private set; }
	//TODO public Vector3[] approachAngle { get; private set; }

	private bool Enter = false;
	private bool Exit = false;
	private bool Stay = false;

    virtual protected void HitByLightEnter(){}

    virtual protected void HitByLightExit(){}

    virtual protected void HitByLightStay(){}

	public void CallHitByLightEnter()
	{
		Enter = true;
	}

	public void CallHitByLightExit()
	{
		Exit = true;
	}

	public void CallHitByLightStay()
	{
		Stay = true;
		// TODO add vector angles
	}

	void LateUpdate()
	{
		if (Enter && isHitByLight == false)
		{
			timeIlluminated = 0f;
			isHitByLight = true;
			HitByLightEnter();
		}
		
		if (Stay)
		{
			timeIlluminated += Time.deltaTime;
			HitByLightStay();
		}
		else if (Exit && !Enter)
		{
			timeIlluminated = 0f;
			isHitByLight = false;
			HitByLightExit();
		}

		Enter = false;
		Exit = false;
		Stay = false;
		// TODO clear vector angles
	}
}