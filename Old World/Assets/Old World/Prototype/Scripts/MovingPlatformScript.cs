using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 StartTransform;
    private bool MoveToTarget;

    public Vector3 TargetTransform;
    public int Speed;
    
    void Start ()
    {
        MoveToTarget = true;
        StartTransform = gameObject.transform.position;
	}
	
	void Update ()
    {
        if (MoveToTarget == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, TargetTransform, Speed * Time.deltaTime);
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
