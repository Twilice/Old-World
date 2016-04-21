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
		float time = chargeUpTime * Time.deltaTime;
        ChangingColor.g += time;
	}

    public void setColor(Color c)
    {
        ChangingColor = c;
    }
}
