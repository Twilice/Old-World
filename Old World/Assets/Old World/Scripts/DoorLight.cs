using UnityEngine;
using System.Collections;

public class DoorLight : MonoBehaviour
{
    MeshRenderer mr;

    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public void Activate()
    {
        mr.material.SetColor("_EmissionColor", Color.green);
        DynamicGI.SetEmissive(GetComponent<Renderer>(), Color.green * 2.7f);
    }

}
