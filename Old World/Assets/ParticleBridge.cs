using UnityEngine;
using System.Collections;

public class ParticleBridge : MonoBehaviour {

    void Start()
    {
        if (StateController.roomFullyPowered || StateController.SegmentActive(tag))
        {
            Activate();
        }
    }
	public void Activate()
    {
        gameObject.FindChildObject("ParticleBridge").SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.FindChildObject("ParticleBridge").SetActive(false);
    }
}
