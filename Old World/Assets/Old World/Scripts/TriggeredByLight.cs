using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TriggeredByLight : MonoBehaviour {

	public bool isHitByLight{get; private set;}
	public float timeIlluminated { get; private set; }
	public List<Vector3> lightRaysDir { get; private set; }
	public List<Vector3> lightRaysPos { get; private set; }
	public TriggeredByLight()
	{
		lightRaysDir  = new List<Vector3>();
		lightRaysPos = new List<Vector3>();
	}
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

	public void CallHitByLightStay(Vector3 lightAngle, Vector3 lightHitPos)
	{
		Stay = true;
		lightRaysDir.Add(lightAngle);
		lightRaysPos.Add(lightHitPos);
	}

	void LateUpdate() // remember to call base.LateUpdate() if it is overriden
	{
        if (Stay)
        {
            if (Enter && isHitByLight == false)
            {
                timeIlluminated = 0f;
                isHitByLight = true;
                HitByLightEnter();
            }
            timeIlluminated += Time.deltaTime;
            HitByLightStay();
        }
		else if (isHitByLight)
		{
			timeIlluminated = 0f;
			isHitByLight = false;
			HitByLightExit();
		}

		Enter = false;
		Exit = false;
		Stay = false;
		lightRaysDir.Clear();
		lightRaysPos.Clear();
	}
}