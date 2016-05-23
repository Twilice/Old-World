using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 StartTransform;
    private bool MoveToTarget;
	private Animator anim;

    public Vector3 TargetTransform;
    public int Speed;
    public bool Elevator;
	public bool Obstacle;
	public bool ReturnToOriginalPosition;
    [HideInInspector]
    public bool returning = false;

	//private bool Activated;
    
    void Start ()
    {
		//Activated = false;
		anim = GetComponent<Animator>();
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
			anim.SetTrigger("turnOn");
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Take 001"))
			{
				anim.SetTrigger("beIdle");
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
			anim.SetTrigger("turnOff");
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Take 001 0"))
			{
				anim.SetTrigger("beIdle");
			}
		}
	}
}