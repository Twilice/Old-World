using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class GeneratorScript : MonoBehaviour
{
    public List<GameObject> Targets;
    public bool PlatformTarget;
    public bool GeneratorTarget;
    public bool ChargerTarget;
    public bool Active = false;

    private DoorLight doorLight;
    private DoorLight[] allDoorLights;

    void Awake()
    {
        allDoorLights = FindObjectsOfType<DoorLight>();
        for (int i = 0; i < allDoorLights.Length; i++)
        {
			if (allDoorLights[i].transform.CompareTag(transform.tag))
            {
                doorLight = allDoorLights[i];
            }
        }
    }

    void Update()
    {
		if (Active == true)
        {
			//Debug.Log(gameObject.name + " online");
			for (int i = 0; i < Targets.Count; i++)
            {
				if (PlatformTarget == true)
				{
					foreach (MovingPlatformScript movingScript in Targets[i].GetComponentsInChildren<MovingPlatformScript>())
					{
						movingScript.Activate();
					}
				}
				if (ChargerTarget == true)
				{
					Targets[i].GetComponent<ChargerScript>().Activate();
				}
			}
		}
    }

    public void Activate()
    {
        //Change bool to true
        Active = true;

        //Activate all generator targets
        for (int i = 0; i < Targets.Count; i++)
        {
            if (GeneratorTarget == true)
            {
                Targets[i].GetComponent<GeneratorScript>().Activate();
            }
        }

		//Change the light
		doorLight.Activate();
    }
}
