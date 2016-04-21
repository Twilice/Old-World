using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour 
{
    private bool charged = false;


	void Start ()
    {
        
	}

    public void setBatteryCharged(bool b)
    {
        charged = b;
    }

	void Update () 
	{

    }
    
    void OnTriggerStay(Collider coll)
    {
		if (coll.gameObject.CompareTag("Player"))
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				gameObject.transform.SetParent(coll.gameObject.transform);
				gameObject.transform.localPosition = new Vector3(0, 1, 1);
			}
		}

		if (coll.gameObject.CompareTag("Charger"))
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				gameObject.transform.SetParent(coll.gameObject.transform);
				gameObject.transform.localPosition = new Vector3(0, 0, 0.5F);
				gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
				gameObject.transform.localScale = new Vector3(0.8F, 0.8F, 1.5F);
			}
		}
    }
}
