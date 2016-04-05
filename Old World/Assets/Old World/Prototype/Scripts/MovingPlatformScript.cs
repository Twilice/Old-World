using UnityEngine;
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
    
    void Start ()
    {
        MoveToTarget = true;
        RotateToTarget = true;
        StartTransform = gameObject.transform.position;
        StartRotation = gameObject.transform.rotation;

    }
	
	void Update ()
    {
        Debug.Log("Object: " + gameObject.transform.rotation);
        Debug.Log("Target: " + TargetRotation);
        Debug.Log("RotateToTarget: " + RotateToTarget);

        if (RotateToTarget == true && Rotate == true)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, TargetRotation, Speed * Time.deltaTime);

            if (gameObject.transform.rotation == TargetRotation)
            {
                RotateToTarget = false;
            }
        }

        if (MoveToTarget == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, TargetTransform, Speed * Time.deltaTime);
            
            if (gameObject.transform.position == TargetTransform)
            {
                MoveToTarget = false;
            }
        }

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
