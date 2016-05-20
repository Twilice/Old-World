using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spriteChanger : MonoBehaviour {
    public Sprite newSprite;
    public int whenToChangeSprite;
    private bool onlyOnce = false;//Because I am a bad programer
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (IntroScript.instance.finalImageSteps >= whenToChangeSprite && onlyOnce == false)
        {
            GetComponent<SpriteRenderer>().sprite = newSprite;
            onlyOnce = true;
        }
	}
}
