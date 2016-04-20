using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class LensReflect : MonoBehaviour {

    GameObject lens;
    private MoveLensTarget moveLensScript;
    private bool inLight;

    void Start()
    {
        lens = transform.Find("ReflectedLensLight").gameObject;
        moveLensScript = GameObject.Find("MainCamera").GetComponent<MoveLensTarget>();
        inLight = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightShaft"))
            inLight = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightShaft"))
            inLight = false;
    }

    void Update()
    {
        if (inLight && moveLensScript.LensActivated)
            lens.SetActive(true);
        else
            lens.SetActive(false);
    }

}
