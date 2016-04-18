using UnityEngine;
using System.Collections;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> refs/remotes/origin/master

public abstract class TriggeredByLight : MonoBehaviour {

	public bool isHitByLight{get; private set;}
	public float timeIlluminated { get; private set; }
<<<<<<< HEAD
	//TODO public Vector3[] approachAngle { get; private set; }

	private bool Enter = false;
	private bool Exit = false;
=======
	public List<Vector3> lightRaysDir { get; private set; }
	public List<Vector3> lightRaysPos { get; private set; }
	public TriggeredByLight()
	{
		lightRaysDir  = new List<Vector3>();
		lightRaysPos = new List<Vector3>();
	}
	private bool Enter = false;
>>>>>>> refs/remotes/origin/master
	private bool Stay = false;

    virtual protected void HitByLightEnter(){}

    virtual protected void HitByLightExit(){}

    virtual protected void HitByLightStay(){}

	public void CallHitByLightEnter()
	{
		Enter = true;
	}

<<<<<<< HEAD
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
=======
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
>>>>>>> refs/remotes/origin/master
		{
			timeIlluminated = 0f;
			isHitByLight = false;
			HitByLightExit();
		}

		Enter = false;
<<<<<<< HEAD
		Exit = false;
		Stay = false;
		// TODO clear vector angles
=======
		Stay = false;
		lightRaysDir.Clear();
		lightRaysPos.Clear();
>>>>>>> refs/remotes/origin/master
	}
}