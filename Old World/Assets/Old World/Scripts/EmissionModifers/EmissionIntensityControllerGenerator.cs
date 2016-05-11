using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class EmissionIntensityControllerGenerator : MonoBehaviour
{

	[Range(0, 5)]
	public float maxIntensity = 2.7f;
	[Range(1, 9)]
	public int lightNumber;


	private EventPlayer sfx;
	private Renderer r;
	private float emissionIntensity = 0f;
	private Color c;
	private MeshRenderer mr;
	private float energy = 0.0f;
	private bool activated = false;

	void Awake()
	{
		sfx = GetComponent<EventPlayer>();
		r = GetComponent<Renderer>();
		mr = GetComponent<MeshRenderer>();
		if (lightNumber == 9)
		{
			c = Color.red;
			energy = 1.0f;
		}
		else
		{
			c = Color.white;
		}
	}
	// Update is called once per frame
	void Update()
	{
		emissionIntensity = maxIntensity * energy;
		if (RoomState.roomFullyPowered && lightNumber == 9)
		{
			mr.material.SetColor("_EmissionColor", Color.green * emissionIntensity / 2f);
		}
		mr.material.SetColor("_EmissionColor", c * emissionIntensity / 2.0f);
		DynamicGI.SetEmissive(r, c * emissionIntensity);
	}
	bool sfx_active = false;
	//When the solarpanel is hit
	public void LerpEnergy(float solarPanelEnergy)
	{
		if (solarPanelEnergy == 1)
		{
			energy = 1;
			activated = true;
			c = Color.green;
		}

		if (solarPanelEnergy > 0.1f && sfx != null && !activated && !sfx_active)
		{
			sfx.PlayEvent();
			sfx_active = true;
		}
		if (solarPanelEnergy < 0.1f && sfx != null && !activated && sfx_active)
		{
			sfx_active = false;
		}
		if (sfx != null && !activated)
		{
			sfx.ChangeParameter("Tones", solarPanelEnergy + 0.01f);
		}
		if (!activated)
		{
			if (lightNumber < 9)
			{
				if (solarPanelEnergy >= (float)lightNumber / 9.0f)
				{
					energy = 1f;
				}
				else
				{
					energy = 0f;
				}
			}
		}
	}
}
