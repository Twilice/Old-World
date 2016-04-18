using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MirrorLightRay : TriggeredByLight {

	[Header("LightRayReflection")]
	public GameObject LightRayPrefab;
	private List<GameObject> rays = new List<GameObject>();

	void Start()
	{
		if (LightRayPrefab == null)
			Debug.LogError("Prefab ReflectedLight missing");
	}
	protected override void HitByLightStay()
	{
		for(int i = 0; i < lightRaysDir.Count; i++)
		{
			if(rays.Count <= i)
			{
				rays.Add(GameObject.Instantiate(LightRayPrefab));
				rays[i].transform.parent = transform;
				rays[i].transform.localPosition = Vector3.zero;
				rays[i].transform.localEulerAngles = Vector3.zero;
			}
			rays[i].SetActive(true);
			Vector3 newLightDir = Vector3.Reflect(lightRaysDir[i], transform.forward);
			rays[i].transform.position = lightRaysPos[i] + newLightDir*0.1f;
			rays[i].transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Reflect(lightRaysDir[i], transform.forward));
		}

		for (int i = lightRaysDir.Count; i < rays.Count; i++)
		{
			rays[i].SetActive(false);
		}
	}

	protected override void HitByLightExit()
	{
		for (int i = 0; i < rays.Count; i++)
		{
			rays[i].SetActive(false);
		}
	}
}
