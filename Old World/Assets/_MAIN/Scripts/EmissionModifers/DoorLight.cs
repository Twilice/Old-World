using UnityEngine;
using System.Collections;

public class DoorLight : MonoBehaviour
{
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void Activate()
    {
        rend.material.SetColor("_EmissionColor", Color.green);
        DynamicGI.SetEmissive(rend, Color.green * 2.7f);
    }

}
