using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class EmissionIntensityController : MonoBehaviour
{

    [Range(0, 3)]
    public float roomActiveIntensity = 2.7f;
    [Range(0, 3)]
    public float generatorActiveIntensity = 2.0f;
    [Range(0,20)]
    public float lerpLightTime = 4.0f;

    private float t = 0.0f;
    private Renderer r;
    private float emissionIntensity = 0f;
    private Color c;
    private MeshRenderer mr;

    void Start()
    {
        r = GetComponent<Renderer>();
        mr = GetComponent<MeshRenderer>();
        c = mr.material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomState.roomFullyPowered)
        {
            LerpLight();
        }

        mr.material.SetColor("_EmissionColor", c * emissionIntensity);
        DynamicGI.SetEmissive(r, c * emissionIntensity);
    }

    public void SetEmissionIntesity(float f)
    {
        emissionIntensity = f;
    }

    void LerpLight()
    {
        if (t < 1.0f)
        {
            t += Time.deltaTime * 0.06f; // TODO5 timescale or something is wrong
            emissionIntensity = Mathf.Lerp(generatorActiveIntensity, roomActiveIntensity, t);
        }
    }

    public void LerpLight(float lerpTime)
    {
        float factor = 1 / (lerpTime * 3);
        if (t < 1.0f)
        {
            t += Time.deltaTime * factor; // TODO5 timescale or something is wrong
            emissionIntensity = Mathf.Lerp(generatorActiveIntensity, roomActiveIntensity, t);
        }
    }
}
