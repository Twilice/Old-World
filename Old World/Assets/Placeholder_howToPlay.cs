using UnityEngine;
using System.Collections;

public class Placeholder_howToPlay : MonoBehaviour
{
	public GameObject HTPText;
	public GameObject HTPBackground;
	private bool active = false;

	void Awake()
	{
		HTPText.SetActive(false);
		HTPBackground.SetActive(false);
	}
	
	public void Activate()
	{
		if (active == false)
		{
			HTPText.SetActive(true);
			HTPBackground.SetActive(true);
			active = true;
		}
		else
		{
			HTPText.SetActive(false);
			HTPBackground.SetActive(false);
			active = false;
		}
	}
}
