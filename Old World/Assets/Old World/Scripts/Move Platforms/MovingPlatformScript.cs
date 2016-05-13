﻿using UnityEngine;
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
    [HideInInspector]
    public bool returning = false;

	//private bool Activated;
    
    void Start ()
    {
        //Activated = false;
        MoveToTarget = true;
        StartTransform = gameObject.transform.localPosition;
        if (Elevator == false && (StateController.roomFullyPowered || StateController.SegmentActive(tag)))
        {
            returning = false;
            gameObject.transform.localPosition = TargetTransform;
        }
    }
	
    void Update()
    {
        if(returning)
        {
            MovingBack();
        }
    }

	public void Activate ()
    {
		//Debug.Log("Going places");
		//Activated = true;

		//Moves the gameobject
		if (MoveToTarget == true)
		{
			gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, TargetTransform, Speed * Time.deltaTime);

			if (gameObject.transform.localPosition == TargetTransform)
			{
				MoveToTarget = false;
			}
		}


		//Moves the gameobject back to its starting location
		if (MoveToTarget == false && Elevator == true)
		{
			gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, StartTransform, Speed * Time.deltaTime);

			if (gameObject.transform.localPosition == StartTransform)
			{
				MoveToTarget = true;
			}
		}
	}

	public void MovingBack()
	{
		//Debug.Log("Going back");
		if (gameObject.transform.localPosition != StartTransform)
		{
			gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, StartTransform, Speed * Time.deltaTime);
		}
        else
        {
			MoveToTarget = true;
            returning = false;
        }
	}
}