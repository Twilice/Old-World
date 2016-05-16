using UnityEngine;
using System.Collections;

public class ChargerScript : BatteryUser
{

	public void Activate()
	{
		if (battery != null)
		{
			battery.amountOfCharge += 3;
		}
	}
}
