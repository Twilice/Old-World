using UnityEngine;
using System.Collections;

public class ParticleBridge : MonoBehaviour {

	public void Activate()
    {
        gameObject.FindChildObject("ParticleBridge").SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.FindChildObject("ParticleBridge").SetActive(false);
    }
}
