using UnityEngine;
using System.Collections;

public class EmitLightRayCast : MonoBehaviour {

<<<<<<< HEAD
	Transform lastHitObject = null;
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 500))
		{
				
=======
	private Transform lastHitObject = null;
	private Vector3 dir = Vector3.zero;
	private Vector3 hitPos = Vector3.zero;
	void Update () {
		RaycastHit hit;
		dir = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(transform.position, dir, out hit, 500))
		{
			hitPos = hit.point;
>>>>>>> refs/remotes/origin/master
			Transform hitObject = hit.transform;
			TriggeredByLight[] scripts = hitObject.GetComponents<TriggeredByLight>();

			if (scripts.Length == 0)
			{
				if(lastHitObject != null)
				{
<<<<<<< HEAD
					LightExit(lastHitObject);
=======
				//	LightExit(lastHitObject);
>>>>>>> refs/remotes/origin/master
					lastHitObject = null;
				}
			}
			else if(hitObject.Equals(lastHitObject))
			{
				LightStay(scripts);
			}
			else
			{
				LightEnter(scripts);
				LightStay(scripts);
				if (lastHitObject != null)
				{
<<<<<<< HEAD
					LightExit(lastHitObject);
=======
				//	LightExit(lastHitObject);
>>>>>>> refs/remotes/origin/master
				}
				lastHitObject = hitObject;
			}
		}
		else
		{
			if (lastHitObject != null)
			{
<<<<<<< HEAD
				LightExit(lastHitObject);
=======
				//LightExit(lastHitObject);
>>>>>>> refs/remotes/origin/master
				lastHitObject = null;
			}
		}
    }

	void OnDisable()
	{
		if (lastHitObject != null)
		{
<<<<<<< HEAD
			LightExit(lastHitObject);
=======
		//	LightExit(lastHitObject);
>>>>>>> refs/remotes/origin/master
			lastHitObject = null;
		}
	}

	private void LightEnter(TriggeredByLight[] scripts)
	{
		foreach(TriggeredByLight script in scripts)
		{
			script.CallHitByLightEnter();
		}
	}
<<<<<<< HEAD
	private void LightExit(TriggeredByLight[] scripts)
=======
	/*private void LightExit(TriggeredByLight[] scripts)
>>>>>>> refs/remotes/origin/master
	{
		foreach (TriggeredByLight script in scripts)
		{
			script.CallHitByLightExit();
		}
<<<<<<< HEAD
	}
=======
	}*/
>>>>>>> refs/remotes/origin/master
	private void LightStay(TriggeredByLight[] scripts)
	{
		foreach (TriggeredByLight script in scripts)
		{
<<<<<<< HEAD
			script.CallHitByLightStay();
=======
			script.CallHitByLightStay(dir, hitPos);
>>>>>>> refs/remotes/origin/master
		}
	}
	private void LightEnter(Transform obj)
	{
		foreach (TriggeredByLight script in obj.GetComponents<TriggeredByLight>())
		{
			script.CallHitByLightEnter();
		}
	}
<<<<<<< HEAD
	private void LightExit(Transform obj)
=======
	/*private void LightExit(Transform obj)
>>>>>>> refs/remotes/origin/master
	{
		foreach (TriggeredByLight script in obj.GetComponents<TriggeredByLight>())
		{
			script.CallHitByLightExit();
		}
<<<<<<< HEAD
	}
=======
	}*/
>>>>>>> refs/remotes/origin/master
	private void LightStay(Transform obj)
	{
		foreach (TriggeredByLight script in obj.GetComponents<TriggeredByLight>())
		{
<<<<<<< HEAD
			script.CallHitByLightStay();
=======
			script.CallHitByLightStay(dir, hitPos);
>>>>>>> refs/remotes/origin/master
		}
	}
}
