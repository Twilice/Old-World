using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIOrb : MonoBehaviour {

    private Image orb;
    public Sprite orbInactive;
    public Sprite orbActive;
    private Material mat;
    void Start()
    {
        orb = GameObject.Find("Crystal").GetComponent<Image>();
        mat = orb.material;
        orb.material = null;
    }
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("LightShaft"))
        {
            orb.sprite = orbActive;
            orb.material = mat;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("LightShaft"))
        {
            orb.sprite = orbInactive;
            orb.material = null;
        }
    }
}
