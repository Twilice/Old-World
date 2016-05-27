using UnityEngine;
using System.Collections;

public class Debug_Script_2 : MonoBehaviour
{
    private Renderer ghost;
    private AudioSource flaviaNoise;
    public MovieTexture megaShader;
    
	void Awake ()
    {
        flaviaNoise = GetComponent<AudioSource>();
        ghost = gameObject.GetComponent<Renderer>();
        ghost.material.mainTexture = megaShader;
        megaShader.loop = true;
        flaviaNoise.clip = megaShader.audioClip;
	}
	
    void OnTriggerEnter()
    {
        megaShader.Play();
        flaviaNoise.Play();
    }
}
