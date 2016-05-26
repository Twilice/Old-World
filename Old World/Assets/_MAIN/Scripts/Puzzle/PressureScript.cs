using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressureScript : MonoBehaviour
{
	public List<GameObject> Targets;
	
	public bool PlatformTarget;
	public bool GeneratorTarget;
	public bool ChargerTarget;

	private Vector3 StartPos;
	private Vector3 EndPos;
	private bool StandingOn = false;

	void Awake()
	{
		StartPos = transform.localPosition;
		EndPos = new Vector3(0, 0, 0);
	}

	void Update()
	{
		if (transform.localPosition != StartPos && StandingOn == false)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, StartPos, Time.deltaTime);
		}
	}

    void OnTriggerEnter(Collider coll)
    {
        StandingOn = true;
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Lens"))
        {
            GeneratorScript generator;
            for (int i = 0; i < Targets.Count; i++)
            {
               // if (PlatformTarget == true)
               // {
                    foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
                    {
                        movingScript.Activate();
                    }
                // }
                generator = Targets[i].GetComponent<GeneratorScript>();
                if (generator != null)
                {
                    generator.Activate();
                }
                //if (ChargerTarget == true)
                //{
                //	Targets[i].GetComponent<ChargerScript>().Activate();
                //}
            }
        }
    }

	void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Lens"))
        {
            if (transform.localPosition != EndPos)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, EndPos, Time.deltaTime);
            }
        }
	}

	void OnTriggerExit(Collider coll)
	{
		StandingOn = false;
		if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Lens"))
        {
			for (int i = 0; i < Targets.Count; i++)
			{
				foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
				{
                    movingScript.Deactivate();
				}
				//if (ChargerTarget == true)
				//{
				//	Targets[i].GetComponent<ChargerScript>().enabled = false;
				//}
			}
		}
		
	}
}
