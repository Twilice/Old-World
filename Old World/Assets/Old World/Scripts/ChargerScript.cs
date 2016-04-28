using UnityEngine;
using System.Collections;

public class ChargerScript : MonoBehaviour
{
	[HideInInspector]
	public bool hasBattery = false;

	public void Activate()
	{
		if (hasBattery == true)
		{
			gameObject.GetComponentInChildren<Battery>().amountOfCharge++;
		}
	}
}
