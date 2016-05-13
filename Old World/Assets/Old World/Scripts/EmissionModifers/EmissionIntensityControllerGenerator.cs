using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class EmissionIntensityControllerGenerator : MonoBehaviour
{

	[Range(0, 5)]
	public float maxIntensity = 2.7f;
	[Range(1, 9)]
	public int lightNumber;


	private sfx s_player;
	private Renderer r;
	private float emissionIntensity = 0f;
	private Color c;
	private MeshRenderer mr;
	private float energy = 0.0f;
	private bool activated = false;

	void Awake()
	{
		s_player = GetComponent<sfx>();
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

    void Start()
    {
        if (StateController.roomFullyPowered)
        {
            energy = 1f;
            activated = true;
            c = Color.green;
        }
        else if (StateController.SegmentActive(tag))
        {
            energy = 1f;
            activated = true;
            c = Color.green;
        }
    }
	// Update is called once per frame
	void Update()
	{
		emissionIntensity = maxIntensity * energy;
		if (StateController.roomFullyPowered && lightNumber == 9)
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
			if (s_player != null && !activated)
			{
				s_player.Play(1);
			}
			activated = true;
			c = Color.green;
		}

		if (solarPanelEnergy > 0.1f && s_player != null && !activated && !sfx_active)
		{
			s_player.Play(0);
			sfx_active = true;
		}
		if (solarPanelEnergy < 0.1f && s_player != null && !activated && sfx_active)
		{
			sfx_active = false;
		}
		if (s_player != null && !activated)
		{
			s_player.ChangeParameter(0 ,"Tones", solarPanelEnergy + 0.01f);
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
