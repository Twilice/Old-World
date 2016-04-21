using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 StartTransform;
    private bool MoveToTarget;

    public Vector3 TargetTransform;
    public int Speed;
    public bool Elevator;
	public bool ReturnToOriginalPosition;

	//private bool Activated;
    
    void Start ()
    {
		//Activated = false;
		MoveToTarget = true;
        StartTransform = gameObject.transform.position;
	}
	
	public void Activate ()
    {
		//Debug.Log("Activated");
		//Activated = true;

		//Moves the gameobject
		if (MoveToTarget == true)
		{
			gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.position, TargetTransform, Speed * Time.deltaTime);

			if (gameObject.transform.localPosition == TargetTransform)
			{
				MoveToTarget = false;
			}
		}


		//Moves the gameobject back to its starting location
		if (MoveToTarget == false && Elevator == true)
		{
			gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.position, StartTransform, Speed * Time.deltaTime);
			if (gameObject.transform.localPosition == StartTransform)
			{
				MoveToTarget = true;
			}
		}
	}

	public void Deactivate()
	{
		//Debug.Log("Deactivated");
		//Activated = false;
		MovingBack();
	}

	public void MovingBack()
	{
		while (gameObject.transform.localPosition != StartTransform)
		{
			gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.position, StartTransform, Speed * Time.deltaTime);
		}
	}
}