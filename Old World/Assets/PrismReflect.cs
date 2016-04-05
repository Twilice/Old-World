using UnityEngine;
using System.Collections;

public class PrismReflect : MonoBehaviour {

    GameObject spotlight;

    void Start()
    {
        foreach(Transform go in gameObject.GetComponentsInChildren<Transform>(true))
            if (go.tag.Equals("LightShaft"))
                spotlight = go.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("LightShaft"))
            spotlight.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("LightShaft"))
            spotlight.SetActive(false);
    }

}
