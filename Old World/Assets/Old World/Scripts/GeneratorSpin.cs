using UnityEngine;
using System.Collections;

public class GeneratorSpin : MonoBehaviour
{
	private float rotationSpeed;
	private bool RotationIncrease;

	public float MaxRotationSpeed;

	void Start ()
	{
		rotationSpeed = 0;
		RotationIncrease = true;
    }
	
	void Update ()
	{
		gameObject.transform.Rotate(new Vector3 (0, rotationSpeed, 0));

		if (RotationIncrease == true)
		{
			rotationSpeed += 0.1F;
			if (rotationSpeed >= MaxRotationSpeed)
			{
				RotationIncrease = false;
			}
		}
	}
}
