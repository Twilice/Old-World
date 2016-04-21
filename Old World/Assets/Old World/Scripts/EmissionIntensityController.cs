using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class EmissionIntensityController : MonoBehaviour
{

    [Range(0, 3)]
    public float roomActiveIntensity = 2.7f;
    [Range(0, 3)]
    public float generatorActiveIntensity = 1.0f;
    [Range(0,20)]
    public float lerpLightTime = 4.0f;

    private float t = 0.0f;
    private float t1 = 0.0f;
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
        if (t1 < 1.0f)
        {
            t1 += Time.deltaTime * 0.2f;
            emissionIntensity = Mathf.Lerp(generatorActiveIntensity, roomActiveIntensity, t1);
        }
    }

    public void LerpLight(float lerpTime)
    {
        float factor = 1 / (lerpTime);
        if (t < 1.0f)
        {
            t += Time.deltaTime * factor;
            emissionIntensity = Mathf.Lerp(0, generatorActiveIntensity, t);
        }
    }
}
