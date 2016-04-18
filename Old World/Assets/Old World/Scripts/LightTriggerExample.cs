using UnityEngine;
using System.Collections;

public class LightTriggerExample : TriggeredByLight {

	protected override void HitByLightStay()
	{
		// do stuff
	}

	protected override void HitByLightExit()
	{
		// do stuff
	}

	protected override void HitByLightEnter()
	{
		// do stuff
	}

	private Material mat;
	void Start()
	{
		mat = GetComponent<MeshRenderer>().material;
	}

	void Update()
	{
<<<<<<< HEAD
		if(timeIlluminated > 10)
=======
		if(timeIlluminated > 3)
>>>>>>> refs/remotes/origin/master
		{
			// do stuff if lighted for more than 1 second
			mat.color = Color.green;
		}
		else if(isHitByLight)
		{
			// do stuff if lighted
			mat.color = Color.blue;
		}
		else
		{
			// do other stuff if not lighted
			mat.color = Color.red;
		}
	}
}
