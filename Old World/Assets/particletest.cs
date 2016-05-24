using UnityEngine;
using System.Collections;

public class particletest : MonoBehaviour {

    ParticleSystem.Particle[] unused = new ParticleSystem.Particle[1];
 
	
    //unity 5.3.1 minifix :'(
	void Update () {
        GetComponent<ParticleSystemRenderer>().enabled = GetComponent<ParticleSystem>().GetParticles(unused) > 0;
	}

    public void PlayParticles()
    {
        GetComponent<ParticleSystemRenderer>().enabled = true;
        GetComponent<ParticleSystem>().Play();
    }
}
