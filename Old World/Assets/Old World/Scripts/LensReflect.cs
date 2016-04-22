using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class LensReflect : MonoBehaviour {

    GameObject lensLight;
    private MoveLensTarget moveLensScript;
    private bool inLight;

    void Start()
    {
        lensLight = transform.Find("ReflectedLensLight").gameObject;
        moveLensScript = GameObject.Find("MainCamera").GetComponent<MoveLensTarget>();
        inLight = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightShaft"))
        {
            inLight = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightShaft"))
        {
            inLight = false;
        }
    }

    void Update()
    {
        if (inLight && moveLensScript.LensActivated)
            lensLight.SetActive(true);
        else
            lensLight.SetActive(false);
    }

}
