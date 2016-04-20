using UnityEngine;
using System.Collections;

public class Charge_bar : MonoBehaviour
{
	private Renderer r;
	public Color ChangingColor = Color.black;

	void Start ()
	{
		r = GetComponent<Renderer>();
	}
	
	void Update ()
	{
		r.material.color = ChangingColor;
	}

	public void PowerTurnedOn(float chargeUpTime)
	{
		float Time = chargeUpTime / 50;
        ChangingColor.g += Time;
	}
}
