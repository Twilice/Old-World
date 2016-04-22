using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour 
{
   // private bool charged = false;
	[HideInInspector]
	public int amountOfCharge;
	[Range(1, 10)]
	public int maxChargeInSeconds;

/*    public void setBatteryCharged(bool b)
    {
        charged = b;
    }*/

	void Update ()
	{
		if (amountOfCharge >= (maxChargeInSeconds * 60))
		{
			GetComponent<Renderer>().material.color = Color.green;
		}
		else if (amountOfCharge < (maxChargeInSeconds * 60))
		{
			GetComponent<Renderer>().material.color = Color.blue;
		}

		if (amountOfCharge >= (maxChargeInSeconds * 60))
		{
			amountOfCharge = maxChargeInSeconds * 60;
		}
    }
    
    void OnTriggerStay(Collider coll)
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (coll.gameObject.CompareTag("Player"))
			{
				gameObject.transform.SetParent(coll.gameObject.transform);
				gameObject.transform.localPosition = new Vector3(0, 1, 1);
			}

			else if (coll.gameObject.CompareTag("Battery Slot"))
			{
				coll.GetComponent<BatterySlot>().hasBattery = true;
				gameObject.transform.SetParent(coll.gameObject.transform);
				gameObject.transform.localPosition = new Vector3(0.5F, 0, 0);
			}
			
			else if (coll.gameObject.CompareTag("Charger"))
			{
				coll.GetComponent<ChargerScript>().hasBattery = true;
				gameObject.transform.SetParent(coll.gameObject.transform);
				gameObject.transform.localPosition = new Vector3(0, 0, 0.5F);
				gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
				gameObject.transform.localScale = new Vector3(0.8F, 0.8F, 1.5F);
			}

			
		}
    }
}
