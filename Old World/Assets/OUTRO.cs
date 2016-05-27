using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OUTRO : MonoBehaviour {
    GUITexture texture;
    // Use this for initialization
    void Start () {
        //texture = GameObject.Find("_Camera").GetComponent<GUITexture>();
        // Set the texture so that it is the the size of the screen and covers it.
       // texture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        
    }
    private bool gameEnded = false;
    private float fadeVal = 0f;
    public float fadeOutSpeed = 0.33f;
    // Update is called once per frame
    void LateUpdate () {
        if (gameEnded)
        {
            FadeToBlack_inner();
        }
    }

    void OnTriggerEnter()
    {
        gameEnded = true;
        texture.enabled = true;
     
    }

    void FadeToBlack_inner()
    {

    

        fadeVal += fadeOutSpeed * Time.deltaTime;
        // Lerp the colour of the texture between itself and black.
        //texture.color = Color.Lerp(Color.clear, Color.black, fadeVal);

        // Make sure the texture is enabled.
        GameObject.Find("OUTROTEXT").GetComponent<Text>().color = new Color(
              GameObject.Find("OUTROTEXT").GetComponent<Text>().color.r,
              GameObject.Find("OUTROTEXT").GetComponent<Text>().color.g,
              GameObject.Find("OUTROTEXT").GetComponent<Text>().color.b,
              fadeVal);

        GameObject.Find("OUTROCOLOR").GetComponent<Image>().color = new Color(
            GameObject.Find("OUTROCOLOR").GetComponent<Image>().color.r,
            GameObject.Find("OUTROCOLOR").GetComponent<Image>().color.g,
            GameObject.Find("OUTROCOLOR").GetComponent<Image>().color.b,
            fadeVal);

        if (fadeVal > 0.95f)
        {
           // texture.color = Color.black;
            fadeVal = 1f;
        }
    }
}
