using UnityEngine;
using System.Collections;

public class DoorLight : MonoBehaviour
{
    Renderer rend;
    public Rooms linkedTo;
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
