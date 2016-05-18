using UnityEngine;
using System.Collections;

public class spriteFade : MonoBehaviour {
    
    public float duration = 5.0f;

    public SpriteRenderer sprite;
    public int fileToFadeAt;
    private float a = 1.0f;
    private float c;

    private bool once = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (IntroScript.instance.currentTextFileID >= fileToFadeAt)
        {
            /*if (once == false)
            {
                a = 0.999f;
                once = true;
            }*/
            //float t = Time.deltaTime / duration;

            //sprite.color = Color.Lerp(GetComponent<Renderer>().material.color, new Color(0f, 0f, 0f, 0f), Time.deltaTime * 100f);// 
            //sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(maximum, minimum, ));

            /*a = Mathf.PingPong(Time.time / duration, 1.0f);
            sprite.color = new Color(1f, 1f, 1f, a);
            if (a <= 0.01f)
            {
                Destroy(this.gameObject);
            }*/
            c = c + Time.deltaTime * duration;
            float t = Mathf.SmoothStep(1f, 0f, c);
            sprite.color = new Color(1f, 1f, 1f, t);


        }
    }
}
