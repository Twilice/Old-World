using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 StartTransform;
    private Quaternion StartRotation;
    private bool MoveToTarget;

    public Vector3 TargetTransform;
    public Quaternion TargetRotation;
    public Transform Target;
    public int Speed;
    public bool Rotate;
    
    void Start ()
    {
        MoveToTarget = true;
        StartTransform = gameObject.transform.position;
        StartRotation = gameObject.transform.rotation;

    }
	
	void Update ()
    {
        if (MoveToTarget == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, TargetTransform, Speed * Time.deltaTime);
            if (Rotate == true)
            {
                //gameObject.transform.rotation = Vector3.RotateTowards(gameObject.transform.rotation, TargetRotation, Speed * Time.deltaTime, 0.0F);
            }
            if (gameObject.transform.position == TargetTransform)
            {
                MoveToTarget = false;
            }
        }

        if (MoveToTarget == false)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, StartTransform, Speed * Time.deltaTime);
            if (gameObject.transform.position == StartTransform)
            {
                MoveToTarget = true;
            }
        }
	}
}
