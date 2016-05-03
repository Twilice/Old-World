using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			//Open door
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//Close door
		}
	}
}
