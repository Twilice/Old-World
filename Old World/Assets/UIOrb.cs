using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIOrb : MonoBehaviour {

    private Image orb;
    public Sprite orbInactive;
    public Sprite orbActive;
    void Start()
    {
        orb = GameObject.Find("Crystal").GetComponent<Image>();
    }
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
