using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour
{
    public Light lgt;

    private bool LightSwitch;

	void Start ()
    {
        lgt = GetComponent<Light>();

        LightSwitch = false;

    }
	
	void Update ()
    {
        if (LightSwitch == false)
        {
            lgt.intensity += 2 * Time.deltaTime;
            if (lgt.intensity >= 8)
            {
                LightSwitch = true;
            }
        }
        if (LightSwitch == true)
        {
            lgt.intensity -= 2F * Time.deltaTime;
            if (lgt.intensity < 3)
            {
                LightSwitch = false;
            }
        }
    }
}
