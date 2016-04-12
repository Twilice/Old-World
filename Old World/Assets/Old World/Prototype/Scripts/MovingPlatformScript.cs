﻿using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 StartTransform;
    private Quaternion StartRotation;
    private bool MoveToTarget;
    private bool RotateToTarget;

    public Vector3 TargetTransform;
    public Quaternion TargetRotation;
    public int Speed;
    public bool Rotate;
    public bool Elevator;
	public bool Trigger;
    
    void Start ()
    {
        MoveToTarget = true;
        RotateToTarget = true;
        StartTransform = gameObject.transform.position;
        StartRotation = gameObject.transform.rotation;

    }
	
	void Update ()
    {
		//Rotates the gameobject
		if (RotateToTarget == true && Rotate == true)
		{
			gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, TargetRotation, Speed * Time.deltaTime);

			if (gameObject.transform.rotation == TargetRotation != true)
			{
				RotateToTarget = false;
			}
		}


		//Moves the gameobject
		if (MoveToTarget == true
			)
		{
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, TargetTransform, Speed * Time.deltaTime);

			if (gameObject.transform.position == TargetTransform)
			{
				MoveToTarget = false;
			}
		}


		//Moves the gameobject back to its starting location
		if (MoveToTarget == false && Elevator == true)
		{
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, StartTransform, Speed * Time.deltaTime);
			if (gameObject.transform.position == StartTransform)
			{
				MoveToTarget = true;
			}
		}
	}
}