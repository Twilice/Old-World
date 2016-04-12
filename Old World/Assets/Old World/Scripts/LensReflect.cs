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
        foreach(Transform go in gameObject.GetComponentsInChildren<Transform>(true))
            if (go.tag.Equals("LightReflected"))
                spotlight = go.gameObject;

        inLight = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("LightShaft"))
            inLight = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("LightShaft"))
            inLight = false;
    }

    void Update()
    {
        if (inLight && anim.GetBool("firstPerson"))
            spotlight.SetActive(true);
        else
            spotlight.SetActive(false);
    }

}
