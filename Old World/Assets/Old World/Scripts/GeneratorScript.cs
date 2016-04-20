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

    private EmissionIntensityController[] e;
    private float t = 0.0f;

    void Awake()
    {
        //Find every instance of the EmissionIntensityController script on every GameObject
        e = FindObjectsOfType<EmissionIntensityController>();
    }

    void Update()
    {
        if (Active == true)
        {
            for (int i = 0; i < Targets.Count; i++)
            {
                if (PlatformTarget == true)
                {
                    Targets[i].GetComponent<MovingPlatformScript>().Activate();
                }
                //if (ChargerTarget == true)
                //{
                //	Targets[i].GetComponent<ChargerScript>().Activate();
                //}
            }
        }
    }

    public void Activate()
    {
        //Change color
        GetComponent<Renderer>().material.color = Color.green;

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

        //Turn on all lights with the same tag as this generator
        for (int i = 0; i < e.Length; i++)
        {
            //If the Gameobject has the same tag as the generator
            if (e[i].transform.CompareTag(transform.tag))
            {
                //LerpLight(e[i]);
            }
        }
    }

    void LerpLight(EmissionIntensityController ec)
    {
        if (t < 1.0f)
        {
            //Time 5 sec
            t += Time.deltaTime * 0.1f;
            ec.SetEmissionIntesity(Mathf.Lerp(0f, 3f, t));
        }
    }
}
