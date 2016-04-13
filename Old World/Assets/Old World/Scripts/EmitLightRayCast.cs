using UnityEngine;
using System.Collections;

public class EmitLightRayCast : MonoBehaviour {

	private Transform lastHitObject = null;
	private Vector3 dir = Vector3.zero;
	private Vector3 hitPos = Vector3.zero;
	void Update () {
		RaycastHit hit;
		dir = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(transform.position, dir, out hit, 500))
		{
			hitPos = hit.point;
			Transform hitObject = hit.transform;
			TriggeredByLight[] scripts = hitObject.GetComponents<TriggeredByLight>();

			if (scripts.Length == 0)
			{
				if(lastHitObject != null)
				{
				//	LightExit(lastHitObject);
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
				//	LightExit(lastHitObject);
				}
				lastHitObject = hitObject;
			}
		}
		else
		{
			if (lastHitObject != null)
			{
				//LightExit(lastHitObject);
				lastHitObject = null;
			}
		}
    }

	void OnDisable()
	{
		if (lastHitObject != null)
		{
		//	LightExit(lastHitObject);
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
	/*private void LightExit(TriggeredByLight[] scripts)
	{
		foreach (TriggeredByLight script in scripts)
		{
			script.CallHitByLightExit();
		}
	}*/
	private void LightStay(TriggeredByLight[] scripts)
	{
		foreach (TriggeredByLight script in scripts)
		{
			script.CallHitByLightStay(dir, hitPos);
		}
	}
	private void LightEnter(Transform obj)
	{
		foreach (TriggeredByLight script in obj.GetComponents<TriggeredByLight>())
		{
			script.CallHitByLightEnter();
		}
	}
	/*private void LightExit(Transform obj)
	{
		foreach (TriggeredByLight script in obj.GetComponents<TriggeredByLight>())
		{
			script.CallHitByLightExit();
		}
	}*/
	private void LightStay(Transform obj)
	{
		foreach (TriggeredByLight script in obj.GetComponents<TriggeredByLight>())
		{
			script.CallHitByLightStay(dir, hitPos);
		}
	}
}
