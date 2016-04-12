using UnityEngine;
using System.Collections;

public class LensReflect : MonoBehaviour {

    GameObject spotlight;
    private Animator anim;
    private bool inLight;

    void Awake()
    {
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Start()
    {
        spotlight = transform.Find("ReflectedLensLight").gameObject;
        
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
        if (inLight && FirstPersonViewToggle.FirstPerson)
            spotlight.SetActive(true);
        else
            spotlight.SetActive(false);
    }

}
