using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour 
{
	// private bool charged = false;
	[HideInInspector]
	public int amountOfCharge;
	[Range(1, 10)]
	public int maxChargeInSeconds;

	private bool pickedUp = false;

/*    public void setBatteryCharged(bool b)
    {
        charged = b;
    }*/

	void Update ()
	{
		Debug.Log("Current charge: " + amountOfCharge);
		Debug.Log("Max charge: " + (maxChargeInSeconds * 60));

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

		if (amountOfCharge < 0)
		{
			amountOfCharge = 0;
		}
	}
    
    void OnTriggerStay(Collider coll)
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (pickedUp == false)
			{
				if (coll.gameObject.CompareTag("Player"))
				{
					if (gameObject.GetComponentInParent<ChargerScript>() == true)
					{
						gameObject.GetComponentInParent<ChargerScript>().hasBattery = false;
					}

					if (gameObject.GetComponentInParent<BatterySlot>() == true)
					{
						gameObject.GetComponentInParent<BatterySlot>().hasBattery = false;
					}

					if (gameObject.GetComponentInParent<BatterySlotLinked>() == true)
					{
						gameObject.GetComponentInParent<BatterySlotLinked>().hasBattery = false;
					}

					pickedUp = true;
					gameObject.transform.SetParent(coll.gameObject.transform);
					gameObject.transform.localPosition = new Vector3(0, 1, 1);
				}
			}

			if (pickedUp == true)
			{
				if (coll.gameObject.CompareTag("Battery Slot") && coll.GetComponent<BatterySlot>().hasBattery == false)
				{
					pickedUp = false;
					coll.GetComponent<BatterySlot>().hasBattery = true;
					gameObject.transform.SetParent(coll.gameObject.transform);
					gameObject.transform.localPosition = new Vector3(0.5F, 0, 0);
				}

				if (coll.gameObject.CompareTag("Charger") && coll.GetComponent<ChargerScript>().hasBattery == false)
				{
					Debug.Log("Battery is in");
					pickedUp = false;
					coll.GetComponent<ChargerScript>().hasBattery = true;
					gameObject.transform.SetParent(coll.gameObject.transform);
					gameObject.transform.localPosition = new Vector3(0, 0, 0.5F);
				}

				if (coll.gameObject.CompareTag("Battery Slot Linked") && coll.GetComponent<BatterySlotLinked>().hasBattery == false)
				{
					pickedUp = false;
					coll.GetComponent<BatterySlotLinked>().hasBattery = true;
					gameObject.transform.SetParent(coll.gameObject.transform);
					gameObject.transform.localPosition = new Vector3(0.5F, 0, 0);
				}
			}
		}
    }
}
