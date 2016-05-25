using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIOrb : MonoBehaviour {

    public Image orb;
    public Sprite orbInactive;
    public Sprite orbActive;

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("LightShaft"))
        {
            orb.sprite = orbActive;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("LightShaft"))
        {
            orb.sprite = orbInactive;
        }
    }
}
