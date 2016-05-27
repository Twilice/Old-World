using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spriteFade2 : MonoBehaviour
{

    public float duration = 5.0f;

    public SpriteRenderer sprite;
  //  private float a = 1.0f;
    private float c;
    private float t;

  //  private bool once = false;
    public bool sceneChanger;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IntroScript.instance.finalImageSteps >= 4)
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
            t = Mathf.SmoothStep(1f, 0f, c);
            sprite.color = new Color(1f, 1f, 1f, t);


        }
        if (sceneChanger == true && IntroScript.instance.textBoxShown == false)
        {
            IntroScript.musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            IntroScript.voiceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            StateController.GameOn = true;
            SceneManager.LoadScene("Corridor");
        }
    }
}
