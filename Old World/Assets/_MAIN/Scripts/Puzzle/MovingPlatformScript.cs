using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class MovingPlatformScript : MonoBehaviour
{
	private Vector3 StartTransform;
	private bool MovingToTarget = true;
	private Animator anim;
	private bool openOnce = true;
	private bool closeOnce = true;

	public Vector3 TargetTransform;
	public int Speed = 3;
	public bool Elevator = false;
	public bool Animating = false;
	public bool ReturnToOriginalPosition = true;
	[HideInInspector]
	public bool returning = false;

	private bool Activated;

    void Start()
	{
		//Activated = false;
		anim = GetComponent<Animator>();
		StartTransform = gameObject.transform.localPosition;
        if ((StateController.roomFullyPowered || StateController.SegmentActive(tag)))
        {
            if (Elevator == false)
            {
                MovingToTarget = false;
                returning = true;
                gameObject.transform.localPosition = TargetTransform;
                Activated = true;
                if (Animating)
                    anim.SetBool("Active", true);
            }
            else
            {
                Activated = true;
                if (Animating)
                    anim.SetBool("Active", true);
            }
        }
	}
  
    void Update()
    {
        if(Activated)
        {
            if(MovingToTarget == true)
            {
                MoveToTarget();
            }
            else if (Elevator && MovingToTarget == false)
            {
                MoveToOriginal();
            }
        }
        else //not powered
        {
            if(ReturnToOriginalPosition)
                Return();
        }
    }

    private void MoveToTarget()
    {
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, TargetTransform, Speed * Time.deltaTime);

        if (gameObject.transform.localPosition == TargetTransform)
        {
            MovingToTarget = false;
        }
    }
    private void MoveToOriginal()
    {
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, StartTransform, Speed * Time.deltaTime);

        if (gameObject.transform.localPosition == StartTransform)
        {
            MovingToTarget = true;
        }
    }
    private void Return()
    {
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, StartTransform, Speed * Time.deltaTime);
        MovingToTarget = true;
    }

    public void Activate()
    {
        Activated = true;
        if (Animating)
            anim.SetBool("Active", true);
    }

    public void Deactivate()
    {
        Activated = false;
        if (Animating)
            anim.SetBool("Active", false);
    }

/*	void Update()
	{
		if (returning)
		{
			MovingBack();
		}
	}

	public void Activate()
	{
		//Moves the gameobject
		if (MoveToTarget == true && Obstacle != true)
		{
			gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, TargetTransform, Speed * Time.deltaTime);

			if (gameObject.transform.localPosition == TargetTransform)
			{
				MoveToTarget = false;
			}
		}


		//Moves the gameobject back to its starting location
		if (MoveToTarget == false && Elevator == true && Obstacle != true)
		{
			gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, StartTransform, Speed * Time.deltaTime);

			if (gameObject.transform.localPosition == StartTransform)
			{
				MoveToTarget = true;
			}
		}

		else if (Obstacle)
		{
			if (openOnce)
			{
				anim.SetTrigger("turnOn");
				openOnce = false;
				closeOnce = true;
			}
		}
	}

	public void MovingBack()
	{
		if (Obstacle != true)
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

		else if (Obstacle)
		{
			if (closeOnce)
			{
				anim.SetTrigger("turnOff");
				closeOnce = false;
				openOnce = true;
				returning = false; 
			}
		}
	}
    */
}