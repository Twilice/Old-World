using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HowToPlayScript : MonoBehaviour
{
	public List<GameObject> HTP_Objects;
	
	private bool active = false;

	void Awake()
	{
		for (int i = 0; i < HTP_Objects.Count; i++)
		{
			HTP_Objects[i].SetActive(false);
		}
	}

    public void Activate()
    {
        for (int i = 0; i < HTP_Objects.Count; i++)
        {
            HTP_Objects[i].SetActive(true);
        }
        active = true;
    }

    public void Deactivate()
    {
        for (int i = 0; i < HTP_Objects.Count; i++)
        {
            HTP_Objects[i].SetActive(false);
        }
        active = false;
    }
}
