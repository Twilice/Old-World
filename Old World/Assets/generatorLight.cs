using UnityEngine;
using System.Collections;

public class generatorLight : TriggeredByLight {

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
		if(timeIlluminated > 1)
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
